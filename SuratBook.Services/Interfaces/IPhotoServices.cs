using SuratBook.Services.Models.Photo;

namespace SuratBook.Services.Interfaces
{
    public interface IPhotoServices
    {
        Task<string> CreatePhotoAsync(CreatePhotoModel model);

        Task<bool> FindByPathAsync(string path);

        Task<IEnumerable<PhotoViewModel>> GetPhotosAsync(string id);
    }
}
