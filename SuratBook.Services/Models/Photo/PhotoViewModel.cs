namespace SuratBook.Services.Models.Photo
{
    public class PhotoViewModel
    {
        public string Key { get; set; } = null!;

        public string DropboxId { get; set; } = null!;

        public string DropboxPath { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public int Likes { get; set; }
    }
}
