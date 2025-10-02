using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Models.Trek;
using UserRoles.Services.Interface;

public class TrekAppService : ITrekAppService
{
    private readonly AppDbContext _context;
    public readonly IFileService _fileService;

    public TrekAppService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }


    public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
    {
        var query = _context.TrekPackages.Where(t => t.Slug == slug);
        if (excludeId.HasValue)
        {
            query = query.Where(t => t.Id != excludeId.Value);
        }
        return await query.AnyAsync();
    }


    public async Task<TrekPackage> GetTrekPackageBySlugAsync(string slug, bool includeFaqs = false,
        bool includeImages = false, bool includeItinerary = true, bool includeCostInfo = true)
    {
        var query = _context.TrekPackages.Where(x => x.Slug == slug).AsQueryable();

        if (includeCostInfo)
        {
            query = query.Include(t => t.PackageCostInfo)
                         .ThenInclude(c => c.GroupPricing);
        }

        if (includeItinerary)
        {
            query = query.Include(t => t.trekItineraryDays);
        }

        if (includeFaqs)
        {
            query = query.Include(t => t.FAQs);
        }

        if (includeImages)
        {
            query = query.Include(t => t.TrekPackageImages);
        }

        var trekPackage = await query.FirstOrDefaultAsync();

        if (trekPackage == null)
            throw new KeyNotFoundException($"Trek package with slug '{slug}' not found.");

        return trekPackage;
    }


    public async Task<TrekPackage> CreateTrekPackageAsync(TrekPackage trekPackage)
    {
        var now = DateTime.UtcNow;
        trekPackage.CreatedAt = now;
        trekPackage.UpdatedAt = now;

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.TrekPackages.Add(trekPackage);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return trekPackage;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw; // preserve original EF exception
        }
    }



    public async Task UpdateTrekPackageAsync(TrekPackage trekPackage)
    {
        var existingTrekPackage = await _context.TrekPackages
            .Include(tp => tp.trekItineraryDays)
            .Include(tp => tp.FAQs)
            .Include(tp => tp.PackageCostInfo)
                .ThenInclude(pci => pci.GroupPricing)
            .Include(tp => tp.TrekPackageImages)
            .FirstOrDefaultAsync(tp => tp.Slug == trekPackage.Slug);

        if (existingTrekPackage == null)
        {
            throw new Exception("Trek package not found.");
        }


        existingTrekPackage.Title = trekPackage.Title;
        existingTrekPackage.Slug = trekPackage.Slug;
        existingTrekPackage.Description = trekPackage.Description;
        existingTrekPackage.Country = trekPackage.Country;
        existingTrekPackage.Duration = trekPackage.Duration;
        existingTrekPackage.Difficulty = trekPackage.Difficulty;
        existingTrekPackage.Activity = trekPackage.Activity;
        existingTrekPackage.MaxAltitude = trekPackage.MaxAltitude;
        existingTrekPackage.BestSeason = trekPackage.BestSeason;
        existingTrekPackage.Accomodation = trekPackage.Accomodation;
        existingTrekPackage.Meal = trekPackage.Meal;
        existingTrekPackage.StartEndPoint = trekPackage.StartEndPoint;
        existingTrekPackage.UpdatedAt = DateTime.UtcNow;

        //Overview
        existingTrekPackage.TrekOverview = trekPackage.TrekOverview;
        //highlights
        existingTrekPackage.TrekHighlight = trekPackage.TrekHighlight;
        //Inclusion
        existingTrekPackage.TrekingPackageInclusion = trekPackage.TrekingPackageInclusion;
        //Exclusion
        existingTrekPackage.TrekingPackageExclusion = trekPackage.TrekingPackageExclusion;
        //Packing
        existingTrekPackage.TrekPackingList = trekPackage.TrekPackingList;




        // Update TrekItineraryDays
        if (trekPackage.trekItineraryDays != null)
        {
            // Remove deleted itinerary days
            var existingItineraryIds = existingTrekPackage.trekItineraryDays.Select(i => i.Id).ToList();
            var newItineraryIds = trekPackage.trekItineraryDays.Select(i => i.Id).ToList();
            var itinerariesToDelete = existingTrekPackage.trekItineraryDays
                .Where(i => i.Id > 0 && !newItineraryIds.Contains(i.Id))
                .ToList();

            foreach (var itineraries in itinerariesToDelete)
            {
                _context.TrekItineraryDays.Remove(itineraries);
            }

            foreach (var newItinerary in trekPackage.trekItineraryDays)
            {
                var existingItinerary = existingTrekPackage.trekItineraryDays
                    .FirstOrDefault(i => i.Id == newItinerary.Id && newItinerary.Id > 0);

                if (existingItinerary != null)
                {
                    // Update existing itinerary
                    existingItinerary.DayNumber = newItinerary.DayNumber;
                    existingItinerary.Title = newItinerary.Title;
                    existingItinerary.Description = newItinerary.Description;
                    existingItinerary.UpdatedAt = DateTime.UtcNow;

                }
                else
                {
                    // Add new itinerary
                    newItinerary.TrekPackageId = existingTrekPackage.Id;
                    newItinerary.TrekPackage = existingTrekPackage;
                    _context.TrekItineraryDays.Add(newItinerary);
                }
            }
        }


        // Update FAQs
        if (trekPackage.FAQs != null)
        {
            // Remove deleted FAQs
            var existingFAQIds = existingTrekPackage.FAQs.Select(f => f.Id).ToList();
            var newFAQIds = trekPackage.FAQs.Select(f => f.Id).ToList();
            var faqsToDelete = existingTrekPackage.FAQs
                .Where(f => f.Id > 0 && !newFAQIds.Contains(f.Id))
                .ToList();

            foreach (var faq in faqsToDelete)
            {
                _context.TrekFAQs.Remove(faq);
            }

            // Update or add new FAQs
            foreach (var newFAQ in trekPackage.FAQs)
            {
                var existingFAQ = existingTrekPackage.FAQs
                    .FirstOrDefault(f => f.Id == newFAQ.Id && newFAQ.Id > 0);

                if (existingFAQ != null)
                {
                    // Update existing FAQ
                    existingFAQ.Category = newFAQ.Category;
                    existingFAQ.Question = newFAQ.Question;
                    existingFAQ.Answer = newFAQ.Answer;
                    existingFAQ.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Add new FAQ
                    newFAQ.TrekPackageId = existingTrekPackage.Id;
                    newFAQ.TrekPackage = existingTrekPackage;
                    _context.TrekFAQs.Add(newFAQ);
                }
            }
        }

        // Update PackageCostInfo
        if (trekPackage.PackageCostInfo != null)
        {
            if (existingTrekPackage.PackageCostInfo == null)
            {
                existingTrekPackage.PackageCostInfo = new TrekPackageCostInfo();
            }
            existingTrekPackage.PackageCostInfo.BasePrice = trekPackage.PackageCostInfo.BasePrice;
            existingTrekPackage.PackageCostInfo.Currency = trekPackage.PackageCostInfo.Currency;
            existingTrekPackage.PackageCostInfo.PriceNote = trekPackage.PackageCostInfo.PriceNote;


            // Update GroupPricing
            var existingGroupPricingIds = existingTrekPackage.PackageCostInfo.GroupPricing.Select(gp => gp.Id).ToList();
            var newGroupPricingIds = trekPackage.PackageCostInfo.GroupPricing.Select(gp => gp.Id).ToList();
            var groupPricingsToDelete = existingTrekPackage.PackageCostInfo.GroupPricing
                .Where(gp => gp.Id > 0 && !newGroupPricingIds.Contains(gp.Id))
                .ToList();

            foreach (var groupPricing in groupPricingsToDelete)
            {
                _context.TrekPackageGroupPricings.Remove(groupPricing);
            }

            foreach (var newGroupPricing in trekPackage.PackageCostInfo.GroupPricing)
            {
                var existingGroupPricing = existingTrekPackage.PackageCostInfo.GroupPricing
                    .FirstOrDefault(gp => gp.Id == newGroupPricing.Id && newGroupPricing.Id > 0);

                if (existingGroupPricing != null)
                {
                    // Update existing group pricing
                    existingGroupPricing.MinPeople = newGroupPricing.MinPeople;
                    existingGroupPricing.MaxPeople = newGroupPricing.MaxPeople;
                    existingGroupPricing.PricePerPerson = newGroupPricing.PricePerPerson;
                }
                else
                {
                    // Add new group pricing
                    newGroupPricing.TrekPackageCostInfoId = existingTrekPackage.PackageCostInfo.Id;
                    newGroupPricing.TrekPackageCostInfo = existingTrekPackage.PackageCostInfo;
                    _context.TrekPackageGroupPricings.Add(newGroupPricing);
                }
            }
        }

        //// Update TrekPackageImages
        if (trekPackage.TrekPackageImages != null)
        {
            // Handle deleted images
            var deleteImageIds = trekPackage.TrekPackageImages
                .Where(img => img.Id > 0 && !img.Image.Contains("http"))
                .Select(img => img.Id)
                .ToList();

            foreach (var imageId in deleteImageIds)
            {
                var image = existingTrekPackage.TrekPackageImages.FirstOrDefault(i => i.Id == imageId);
                if (image != null)
                {
                    if (!string.IsNullOrEmpty(image.Image))
                    {
                        await _fileService.DeleteFileAsync(image.Image);
                    }
                    _context.TrekPackageImages.Remove(image);
                }
            }

            // Update or add new images
            foreach (var newImage in trekPackage.TrekPackageImages)
            {
                var existingImage = existingTrekPackage.TrekPackageImages
                    .FirstOrDefault(i => i.Id == newImage.Id && newImage.Id > 0);

                if (existingImage != null)
                {
                    // Update existing image
                    existingImage.Caption = newImage.Caption;
                    existingImage.SubCaption = newImage.SubCaption;
                    existingImage.UpdatedAt = DateTime.UtcNow;
                    if (!string.IsNullOrEmpty(newImage.Image) && newImage.Image != existingImage.Image)
                    {
                        if (!string.IsNullOrEmpty(existingImage.Image))
                        {
                            await _fileService.DeleteFileAsync(existingImage.Image);
                        }
                        existingImage.Image = newImage.Image;
                    }
                }
                else
                {
                    // Add new image
                    newImage.TrekPackageId = existingTrekPackage.Id;
                    newImage.TrekPackage = existingTrekPackage;
                    _context.TrekPackageImages.Add(newImage);
                }
            }
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
    }
    public async Task<List<TrekPackage>> Getall()
    {
        var trekPackage = await _context.TrekPackages.AsNoTracking().ToListAsync();


        return trekPackage;
    }

    public async Task<List<TrekPackage>> TopThreeTrekData()
    {
        var trekPackage = await _context.TrekPackages.OrderBy(x=>x.CreatedAt).Take(3).Include(tp => tp.trekItineraryDays)
            .Include(tp => tp.PackageCostInfo)
                .ThenInclude(pci => pci.GroupPricing)
            .Include(tp => tp.TrekPackageImages).AsNoTracking().ToListAsync();


        return trekPackage;
    }

    public async Task<List<string>> GetallSlug()
    {
        var trekPackage = await _context.TrekPackages.Select(x => x.Slug).AsNoTracking().ToListAsync();


        return trekPackage;
    }



    private void ClearForeignKeyReferences(TrekPackage trekPackage)
    {

        foreach (var file in trekPackage.TrekPackageImages ?? new List<TrekPackageImage>())
            file.TrekPackageId = 0;
    }


}