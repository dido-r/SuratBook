namespace SuratBook.Services.Models.Post
{
    using System.ComponentModel.DataAnnotations;

    public class DeletePostModel
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
