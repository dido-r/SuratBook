namespace SuratBook.Services.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static SuratBook.Data.Constants.Constants;
    using static SuratBook.Data.Constants.ErrorMessages;

    public class CommentFormModel
    {
        [Required]
        [StringLength(CommentMaxLength, ErrorMessage = CommentErrorMessage)]
        public string Content { get; set; } = null!;

        [Required]
        public string PhotoId { get; set; } = null!;
    }
}
