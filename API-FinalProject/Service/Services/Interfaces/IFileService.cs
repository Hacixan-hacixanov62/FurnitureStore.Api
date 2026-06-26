using Microsoft.AspNetCore.Http;

namespace Service.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        public void Delete(string fileName, string folder);
    }
}
