using CustomerManagement.Model;

namespace CustomerManagement.Repositories.IUploadService
{
    public interface IUploadService
    {
        Task<(string imagePath, string fileName,string fileType)> UploadFile(IFormFile imageFile);
    }
}
