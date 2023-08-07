using SuratBook.Services.Models.Post;

namespace SuratBook.Services.Interfaces
{
    public interface IPostServices
    {
        Task<CreatePostResponseModel> CreatePostAsync(CreatePostFormModel model);

        Task EditPostAsync(EditPostFormModel model);

        Task DeletePostAsync(string postId);

        Task LikePostAsync(string userId, string postId);

        Task<IEnumerable<PostViewModel>> GetMyPostAsync(string id, string userId, int offset, int limit);

        Task<IEnumerable<PostViewModel>> GetAllPostsAsync(string userId, int offset, int limit);

        Task<IEnumerable<PostViewModel>> SearchPostsAsync(string name, string userId);
    }
}
