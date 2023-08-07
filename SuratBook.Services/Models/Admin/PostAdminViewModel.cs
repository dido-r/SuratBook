namespace SuratBook.Services.Models.Admin
{
    public class PostAdminViewModel
    {
        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
        public string Creator { get; set; } = null!;
        public string? Image { get; set; }
    }
}
