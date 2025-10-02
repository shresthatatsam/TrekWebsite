namespace UserRoles.Services.Interface
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file, string subFolder);
        Task DeleteFileAsync(string relativePath);
    }
}
