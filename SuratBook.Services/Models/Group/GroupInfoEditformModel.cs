namespace SuratBook.Services.Models.Group
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class GroupInfoEditformModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        [StringLength(GroupNameMaxLength, MinimumLength = GroupNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(GroupInfoMaxLength, MinimumLength = GroupInfoMinLength)]
        public string GroupInfo { get; set; } = null!;
    }
}
