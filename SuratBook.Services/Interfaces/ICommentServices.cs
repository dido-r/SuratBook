using SuratBook.Services.Models.Comment;

namespace SuratBook.Services.Interfaces
{
    public interface ICommentServices
    {
        Task<CommentViewModel> CommentPostAsync(CommentFormModel model, string userId);

        Task<IEnumerable<CommentViewModel>> GetPostCommentsAsync(string postId);
        Task<CommentViewModel> CommentPhotoAsync(CommentFormModel model, string userId);

        Task<IEnumerable<CommentViewModel>> GetPhotoCommentsAsync(string photoId);
    }
}
