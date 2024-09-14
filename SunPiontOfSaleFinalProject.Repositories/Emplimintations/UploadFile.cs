
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;
using System;

namespace SunPiontOfSaleFinalProject.Repositories.Emplimintations
{
    public class UploadFile : IUploadFile
    {
        private IHostingEnvironment _environment;

        public UploadFile(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(string filePath, IFormFile file)
        {
            string UniqueFileName = string.Empty;
            string UploadFolder = _environment.WebRootPath + filePath;
            if (!Directory.Exists(UploadFolder))
            {
                Directory.CreateDirectory(UploadFolder);
            }

            UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string FileName = Path.Combine(UploadFolder, UniqueFileName);

            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                file.CopyToAsync(stream);
                stream.Dispose();
            }
            return filePath + UniqueFileName;
        }
    }
}
