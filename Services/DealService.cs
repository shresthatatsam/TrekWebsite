using System.Reflection.PortableExecutable;
using System;
using Microsoft.EntityFrameworkCore;
using UserRoles.Data;
using UserRoles.Dtos.RequestDtos;
using UserRoles.Dtos.ResponseDtos;
using UserRoles.Models;
using UserRoles.Services.Interface;
using Newtonsoft.Json;

namespace UserRoles.Services
{
    public class DealService : IDealService
    {
        private readonly AppDbContext _context;

        private readonly IFileService _fileService;

        public DealService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }


        public async Task<DealResponseDto> Create(DealRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            string imageUrl = null;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                imageUrl = await _fileService.SaveImageAsync(dto.ImageFile, "Deal-images");

            }

            var featuresList = dto.Features.ToString()?
           .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
           .ToList();


            // Serialize features list to JSON
            var serializedFeatures = JsonConvert.SerializeObject(featuresList);

            var deal = new Deals
            {
                Id = Guid.NewGuid(),
                ImageUrl = imageUrl,
                Caption = dto.Caption,
                SubCaption = dto.SubCaption,
                Isactive = true,
                Details = dto.Details,
                Header = dto.Header,
                Amount = dto.Amount,
                Features = serializedFeatures

            };

            _context.Deals.Add(deal);
            await _context.SaveChangesAsync();


            var deserializedFeatures = JsonConvert.DeserializeObject<List<string>>(serializedFeatures);
            // Prepare and return response DTO
            var responseDto = new DealResponseDto
            {
                Id = deal.Id,
                ImageUrl = deal.ImageUrl,
                Caption = deal.Caption,
                SubCaption = deal.SubCaption,
                Isactive = deal.Isactive,
                Details = deal.Details,
                Header = dto.Header,
                Amount = deal.Amount,
                Features = deserializedFeatures
            };

            return responseDto;
        }


        public async Task<DealResponseDto?> GetById(Guid id)
        {
            return await _context.Deals
                .Where(x => x.Id == id)
                .Select(x => new DealResponseDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    SubCaption = x.SubCaption,
                    Isactive = x.Isactive,
                    Details = x.Details,
                    Header = x.Header,
                    Amount = x.Amount,
                    Features = string.IsNullOrEmpty(x.Features)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(x.Features)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(DealRequestDto dto)
        {
            var entity = await _context.Deals.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null) return false;

               

            entity.Caption = dto.Caption;
            entity.SubCaption = dto.SubCaption;
            entity.Isactive = dto.Isactive;
            entity.Details = dto.Details;
            entity.Header = dto.Header;
            entity.Amount = dto.Amount;

            var featuresList = dto.Features?
       .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
       .Select(f => f.Trim())
       .ToList();

            entity.Features = JsonConvert.SerializeObject(featuresList);

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    await _fileService.DeleteFileAsync(entity.ImageUrl);

                var imageUrl = await _fileService.SaveImageAsync(dto.ImageFile, "Deal-images");
                entity.ImageUrl = imageUrl;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DealResponseDto>> List()
        {
            return await _context.Deals.Where(x =>  x.Isactive).AsNoTracking().Select(x => new DealResponseDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Caption = x.Caption,
                SubCaption = x.SubCaption,
                Isactive = x.Isactive,
                Details = x.Details,
                Header = x.Header,
                Amount = x.Amount,
                Features = string.IsNullOrEmpty(x.Features)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(x.Features)
            }).ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var imageData = await _context.Deals
                .Where(x => x.Id == id)
                .Select(x => new { x.Id, x.ImageUrl })
                .FirstOrDefaultAsync();

            if (imageData.Id == Guid.Empty)
                return false;

            if (!string.IsNullOrEmpty(imageData.ImageUrl))
            {
                await _fileService.DeleteFileAsync(imageData.ImageUrl);
            }

            var entity = new Deals { Id = imageData.Id };
            _context.Deals.Attach(entity);
            _context.Deals.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
