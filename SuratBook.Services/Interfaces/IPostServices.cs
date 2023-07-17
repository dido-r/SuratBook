using SuratBook.Services.Models.Post;

namespace SuratBook.Services.Interfaces
{
    public interface IPostServices
    {
        Task<string> CreatePostAsync(CreatePostFormModel model);

        Task EditPostAsync(EditPostFormModel model);

        Task DeletePostAsync(DeletePostModel model);

        Task<IEnumerable<PostViewModel>> GetAllPostsAsync();
    }
}
