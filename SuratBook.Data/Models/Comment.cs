namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SuratBook.Data.Constants.Constants;

    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwmerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;

        [ForeignKey(nameof(Photo))]
        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;
    }
}
