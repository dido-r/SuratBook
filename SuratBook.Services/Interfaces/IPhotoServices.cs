using SuratBook.Services.Models.Comment;
using SuratBook.Services.Models.Photo;

namespace SuratBook.Services.Interfaces
{
    public interface IPhotoServices
    {
        Task<string> CreatePhotoAsync(CreatePhotoModel model);

        Task<bool> FindByPathAsync(string path);

        Task DeletePhotoAsync(string id);

        Task<string> GetProfileImageAsync(string userId);

        Task SetAsProfileAsync(string userId, string path);

        Task<CommentViewModel> CommentPhotoAsync(CommentFormModel model, string userId);

        Task<IEnumerable<CommentViewModel>> GetPhotoCommentsAsync(string photoId);

        Task LikePhotoAsync(string photoId, string userId);

        Task<IEnumerable<PhotoViewModel>> GetPhotosAsync(string id, string userId);
    }
}
