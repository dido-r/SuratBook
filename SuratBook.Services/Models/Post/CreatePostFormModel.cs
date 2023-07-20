namespace SuratBook.Services.Models.Post
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class CreatePostFormModel
    {
        [Required]
        [StringLength(PostMaxLength, MinimumLength = PostMinLength)]
        public string Description { get; set; } = null!;

        public string? DropboxPath { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        public string? GroupId { get; set; }
    }
}
