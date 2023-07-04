namespace SuratBook.Data.Models
{
    public class Photo
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string DropboxId { get; set; } = null!;

        public string DropboxPath { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;

        public int Likes { get; set; }

        public HashSet<UsersLikedPhotos> UsersLikes { get; set; } = new HashSet<UsersLikedPhotos>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
