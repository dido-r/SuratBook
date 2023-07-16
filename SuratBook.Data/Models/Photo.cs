using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class Photo
    {
        public Guid Id { get; set; }

        [Required]
        public string DropboxId { get; set; } = null!;

        [Required]
        public string DropboxPath { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        [Required]
        public int Likes { get; set; } = 0;

        public HashSet<UsersLikedPhotos> UsersLikes { get; set; } = new HashSet<UsersLikedPhotos>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
