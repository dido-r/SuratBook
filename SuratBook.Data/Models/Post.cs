namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SuratBook.Data.Constants.Constants;

    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(PostMaxLength, MinimumLength = PostMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Photo))]
        public Guid? PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        [Required]
        public int Likes { get; set; } = 0;

        public HashSet<UsersLikedPosts> UsersLikes { get; set; } = new HashSet<UsersLikedPosts>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
