using CustomerManagement.Helper;
using CustomerManagement.Model;
using CustomerManagement.Repositories.IUploadService;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CustomerManagement.Services
{
    public class FileUpload :IUploadService
    {
        public async Task<(string imagePath, string fileName,string fileType)> UploadFile(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filename = Guid.NewGuid() + "_" + imageFile.FileName;

            var fileName = imageFile.FileName;

            var fullPath = Path.Combine(uploadsFolder, filename);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            string imagePath = "/images/" + filename;
            string fileType = imageFile.ContentType;

            return (imagePath, fileName,fileType);


        }


    }
}
