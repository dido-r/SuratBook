namespace SuratBook.Services.ServiceProviders
{
    using Microsoft.EntityFrameworkCore;

    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Comment;

    public class CommentServices : ICommentServices
    {
        private SuratBookDbContext context;

        public CommentServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task<CommentViewModel> CommentPostAsync(CommentFormModel model, string userId)
        {
            var comment = new Comment
            {
                Content = model.Content,
                PostId = Guid.Parse(model.PhotoId),
                OwnerId = Guid.Parse(userId)
            };

            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            var owner = await context.Users.FindAsync(Guid.Parse(userId));
            var ownerName = $"{owner!.FirstName} {owner!.LastName}";

            return new CommentViewModel
            {
                Id = comment.Id.ToString(),
                Content = comment.Content,
                OwnerId = comment.OwnerId.ToString(),
                OwnerName = ownerName,
                OwnerImage = comment.Owner.MainPhoto
            };
        }

        public async Task<IEnumerable<CommentViewModel>> GetPostCommentsAsync(string postId)
        {
            return await context
                .Comments
                .Where(x => x.PostId.ToString() == postId.ToLower())
                .OrderBy(x => x.CreatedOn)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id.ToString(),
                    Content = x.Content,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    OwnerImage = x.Owner.MainPhoto
                })
                .ToListAsync();
        }

        public async Task<CommentViewModel> CommentPhotoAsync(CommentFormModel model, string userId)
        {
            var comment = new Comment
            {
                Content = model.Content,
                PhotoId = Guid.Parse(model.PhotoId),
                OwnerId = Guid.Parse(userId)
            };

            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            var owner = await context.Users.FindAsync(Guid.Parse(userId));
            var ownerName = $"{owner!.FirstName} {owner!.LastName}";

            return new CommentViewModel
            {
                Id = comment.Id.ToString(),
                Content = comment.Content,
                OwnerId = comment.OwnerId.ToString(),
                OwnerName = ownerName,
                OwnerImage = comment.Owner.MainPhoto
            };
        }

        public async Task<IEnumerable<CommentViewModel>> GetPhotoCommentsAsync(string photoId)
        {
            return await context
                .Comments
                .Where(x => x.PhotoId.ToString() == photoId)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id.ToString(),
                    Content = x.Content,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    OwnerImage = x.Owner.MainPhoto
                }).ToListAsync();
        }
    }
}
