namespace SuratBook.Data.Models
{
    public class UsersLikedPhotos
    {
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;
    }
}
