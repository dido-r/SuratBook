namespace SuratBook.Services.Models.Comment
{
    public class CommentViewModel
    {
        public string Id { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string? OwnerImage { get; set; }

    }
}
