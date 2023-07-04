namespace SuratBook.Data.Models
{
    public class UsersLikedPosts
    {
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;
    }
}
