namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Post;

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
