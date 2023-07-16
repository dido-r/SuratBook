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

        public async Task CreatePostAsync(CreatePostFormModel model)
        {
            var post = new Post
            {
                Description = model.Description,
                DropboxPath = model.DropboxPath,
                OwnerId = Guid.Parse(model.OwnerId)
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsAsync()
        {
            var list = await context
                .Posts
                .Select(x => new PostViewModel
                {
                    Key = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    Likes = x.Likes,
                    Comments = x.Comments.Count()
                })
                .ToListAsync();

            return list;
        }
    }
}
