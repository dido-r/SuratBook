namespace SuratBook.Data.Models
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string GroupInfo { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        public GroupAccess Access { get; set; }

        public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

        public HashSet<Photo> Photos { get; set; } = new HashSet<Photo>();

        public HashSet<SuratUser> Members { get; set; } = new HashSet<SuratUser>();
    }
}
