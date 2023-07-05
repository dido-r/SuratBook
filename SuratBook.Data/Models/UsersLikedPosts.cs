using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class UsersLikedPosts
    {
        [Required]
        [ForeignKey(nameof(SuratUser))]
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;
    }
}
