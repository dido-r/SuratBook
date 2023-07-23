namespace SuratBook.Services.Models.Group
{
    public class GroupMediaViewModel
    {
        public string Id { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DropboxPath { get; set; } = null!;

        public int Likes { get; set; }

        public int Comments { get; set; }
    }
}
