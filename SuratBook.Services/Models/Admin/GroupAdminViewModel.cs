namespace SuratBook.Services.Models.Admin
{
    public class GroupAdminViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string GroupInfo { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string? MainPhoto { get; set; }

        public bool IsDeleted { get; set; }
    }
}
