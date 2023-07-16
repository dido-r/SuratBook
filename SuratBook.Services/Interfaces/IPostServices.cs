using SuratBook.Services.Models.Post;

namespace SuratBook.Services.Interfaces
{
    public interface IPostServices
    {
        Task CreatePostAsync(CreatePostFormModel model);

        Task<IEnumerable<PostViewModel>> GetAllPostsAsync();
    }
}
