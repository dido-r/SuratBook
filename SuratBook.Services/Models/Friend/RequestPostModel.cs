namespace SuratBook.Services.Models.Friend
{
    using System.ComponentModel.DataAnnotations;

    public class RequestPostModel
    {
        [Required]
        public string RequsterId { get; set; } = null!;

        [Required]
        public string RecipientId { get; set; } = null!;
    }
}
