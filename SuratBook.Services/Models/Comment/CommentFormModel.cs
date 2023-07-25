namespace SuratBook.Services.Models.Comment
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class CommentFormModel
    {
        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        public string PhotoId { get; set; } = null!;
    }
}
