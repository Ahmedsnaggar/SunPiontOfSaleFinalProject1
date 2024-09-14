using Microsoft.AspNetCore.Http;

namespace SunPiontOfSaleFinalProject.Repositories.Interfaces
{
    public interface IUploadFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
