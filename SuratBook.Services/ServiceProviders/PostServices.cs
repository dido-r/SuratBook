namespace SuratBook.Services.ServiceProviders
{
    using Microsoft.EntityFrameworkCore;
    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Post;

    public class PostServices : IPostServices
    {
        private SuratBookDbContext context;

        public PostServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task<CreatePostResponseModel> CreatePostAsync(CreatePostFormModel model)
        {
            var post = new Post
            {
                Description = model.Description,
                DropboxPath = model.DropboxPath,
                OwnerId = Guid.Parse(model.OwnerId),
                GroupId = model.GroupId != null ? Guid.Parse(model.GroupId) : null
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            var user = await context.Users.FindAsync(Guid.Parse(model.OwnerId));

            return new CreatePostResponseModel
            {
                Id = post.Id.ToString(),
                OwnerImage = user.MainPhoto
            };
        }

        public async Task DeletePostAsync(string postId)
        {
            var post = await context.Posts.FindAsync(Guid.Parse(postId)) ?? throw new ArgumentNullException();
            post.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(EditPostFormModel model)
        {
            var post = await context
                .Posts
                .FindAsync(Guid.Parse(model.Id)) ?? throw new NullReferenceException();

            post.Description = model.Description;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsAsync(string userId, int offset, int limit)
        {
            return await context
                .Posts
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(offset)
                .Take(limit)
                .Select(x => new PostViewModel
                {
                    Key = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    OwnerImage = x.Owner.MainPhoto,
                    GroupName = x.GroupId.HasValue ? x.GroupId.Value.ToString() : null,
                    Likes = x.UsersLikes.Count,
                    Comments = x.Comments.Count,
                    IsLiked = x.UsersLikes.Any(z => z.SuratUserId.ToString() == userId)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetMyPostAsync(string id, string userId, int offset, int limit)
        {
            return await context
                .Posts
                .Where(x => !x.IsDeleted && x.OwnerId.ToString() == id)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(offset)
                .Take(limit)
                .Select(x => new PostViewModel
                {
                    Key = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    OwnerImage = x.Owner.MainPhoto,
                    Likes = x.UsersLikes.Count,
                    Comments = x.Comments.Count,
                    IsLiked = x.UsersLikes.Any(z => z.SuratUserId.ToString() == userId)
                })
                .ToListAsync();
        }

        public async Task LikePostAsync(string userId, string postId)
        {
            var likedPost = new UsersLikedPosts
            {
                SuratUserId = Guid.Parse(userId),
                PostId = Guid.Parse(postId)
            };

            await context.UsersLikedPosts.AddAsync(likedPost);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> SearchPostsAsync(string name, string userId)
        {
            return await context
                .Posts
                .Where(x => !x.IsDeleted && x.OwnerId.ToString() != userId && (x.Description.Contains(name) || x.Owner.FirstName.Contains(name) || x.Owner.LastName.Contains(name) || name.Contains(x.Owner.FirstName) || name.Contains(x.Owner.LastName)))
                .Select(x => new PostViewModel
                {
                    Key = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    GroupName = x.GroupId.HasValue ? x.GroupId.Value.ToString() : null,
                    Likes = x.UsersLikes.Count,
                    Comments = x.Comments.Count,
                    IsLiked = x.UsersLikes.Any(z => z.SuratUserId.ToString() == userId)
                })
                .ToListAsync();
        }
    }
}
