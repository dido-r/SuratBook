namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SuratBook.Data.Constants.Constants;

    public class Group
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(GroupNameMaxLength, MinimumLength = GroupNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(GroupInfoMaxLength, MinimumLength = GroupInfoMinLength)]
        public string GroupInfo { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        [Required]
        public GroupAccess Access { get; set; }

        public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

        public HashSet<Photo> Photos { get; set; } = new HashSet<Photo>();

        public HashSet<UsersJoinedGroups> Members { get; set; } = new HashSet<UsersJoinedGroups>();
    }
}
