namespace SuratBook.Services.Models.Post
{
    public class PostViewModel
    {
        public string Key { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DropboxPath { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string? GroupName { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public bool IsLiked { get; set; }
    }
}
