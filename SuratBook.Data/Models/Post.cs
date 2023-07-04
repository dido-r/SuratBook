namespace SuratBook.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;

        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        public int Likes { get; set; }

        public HashSet<UsersLikedPosts> UsersLikes { get; set; } = new HashSet<UsersLikedPosts>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
