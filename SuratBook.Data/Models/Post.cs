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

        public string? DropboxPath { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        [ForeignKey(nameof(Group))]
        public Guid? GroupId { get; set; } = null!;

        public Group Group { get; set; } = null!;

        public int Likes => UsersLikes.Count;

        public bool IsDeleted { get; set; } = false;

        public HashSet<UsersLikedPosts> UsersLikes { get; set; } = new HashSet<UsersLikedPosts>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
