namespace SuratBook.Services.Models.Photo
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePhotoModel
    {
        [Required]
        public string DropboxId { get; set; } = null!;

        [Required]
        public string DropboxPath { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;
    }
}
