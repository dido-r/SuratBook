namespace SuratBook.Services.ServiceProviders
{
    using Microsoft.EntityFrameworkCore;
    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Comment;
    using SuratBook.Services.Models.Photo;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PhotoServices : IPhotoServices
    {
        private SuratBookDbContext context;

        public PhotoServices(SuratBookDbContext _context)
        {
            context = _context;
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
                OwnerName = ownerName
            };
        }

        public async Task<string> CreatePhotoAsync(CreatePhotoModel model)
        {
            var photo = new Photo
            {
                DropboxId = model.DropboxId,
                DropboxPath = model.DropboxPath,
                OwnerId = Guid.Parse(model.OwnerId)
            };

            await context.Photos.AddAsync(photo);
            await context.SaveChangesAsync();
            return photo.Id.ToString();
        }

        public async Task DeletePhotoAsync(string id)
        {
            var photo = await context.Photos.FindAsync(Guid.Parse(id)) ?? throw new ArgumentNullException();
            context.Photos.Remove(photo);
            await context.SaveChangesAsync();
        }

        public async Task<bool> FindByPathAsync(string path)
        {
            return await context
                .Photos
                .AnyAsync(x => x.DropboxPath == path);
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
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}"
                }).ToListAsync();
        }

        public async Task<IEnumerable<PhotoViewModel>> GetPhotosAsync(string id, string userId)
        {
            return await context
                .Photos
                .Where(x => x.OwnerId.ToString() == id)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new PhotoViewModel
                {
                    Key = x.Id.ToString(),
                    DropboxId = x.DropboxId,
                    DropboxPath = x.DropboxPath,
                    Likes = x.UsersLikes.Count,
                    OwnerId = x.OwnerId.ToString(),
                    Comments = x.Comments.Count,
                    IsLiked = x.UsersLikes.Any(z => z.SuratUserId.ToString() == userId)
                }).ToListAsync();
        }

        public async Task LikePhotoAsync(string photoId, string userId)
        {
            var likedPhoto = new UsersLikedPhotos
            {
                SuratUserId = Guid.Parse(userId),
                PhotoId = Guid.Parse(photoId)
            };

            await context.UsersLikedPhotos.AddAsync(likedPhoto);
            await context.SaveChangesAsync();
        }

        public async Task SetAsProfileAsync(string userId, string path)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            user!.MainPhoto = path;
            await context.SaveChangesAsync();
        }

        public async Task<string> GetProfileImageAsync(string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            return user!.MainPhoto;
        }
    }
}
