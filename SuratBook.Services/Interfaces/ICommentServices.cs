namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Comment;

    public interface ICommentServices
    {
        Task<CommentViewModel> CommentPostAsync(CommentFormModel model, string userId);

        Task<IEnumerable<CommentViewModel>> GetPostCommentsAsync(string postId);
        Task<CommentViewModel> CommentPhotoAsync(CommentFormModel model, string userId);

        Task<IEnumerable<CommentViewModel>> GetPhotoCommentsAsync(string photoId);
    }
}
