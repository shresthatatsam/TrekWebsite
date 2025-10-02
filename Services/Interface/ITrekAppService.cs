using UserRoles.Models.Trek;

namespace UserRoles.Services.Interface
{
    public interface ITrekAppService
    {
        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);
        Task<TrekPackage> GetTrekPackageBySlugAsync(string slug, bool includeFaqs = false,
            bool includeImages = false, bool includeItinerary = true, bool includeCostInfo = true);
        Task<TrekPackage> CreateTrekPackageAsync(TrekPackage trekPackage);
        Task<List<TrekPackage>> Getall();
        Task<List<string>> GetallSlug();
        Task<List<TrekPackage>> TopThreeTrekData();
        Task UpdateTrekPackageAsync(TrekPackage trekPackage);

    }
}
