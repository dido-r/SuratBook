namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UsersLikedPhotos
    {
        [Required]
        [ForeignKey(nameof(SuratUser))]
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Photo))]
        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;
    }
}
