﻿using SuratBook.Services.Models.Post;

namespace SuratBook.Services.Interfaces
{
    public interface IPostServices
    {
        Task<string> CreatePostAsync(CreatePostFormModel model);

        Task EditPostAsync(EditPostFormModel model);

        Task DeletePostAsync(DeletePostModel model);

        Task LikePostAsync(string userId, string postId);

        Task<IEnumerable<PostViewModel>> GetMyPostAsync(string id, string userId);

        Task<IEnumerable<PostViewModel>> GetAllPostsAsync(string userId);
    }
}
