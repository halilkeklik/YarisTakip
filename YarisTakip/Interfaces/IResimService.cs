using CloudinaryDotNet.Actions;

namespace YarisTakip.Interfaces
{
    public interface IResimService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
