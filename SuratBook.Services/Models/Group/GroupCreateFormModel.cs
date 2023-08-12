namespace SuratBook.Services.Models.Group
{
    using System.ComponentModel.DataAnnotations;

    using static SuratBook.Data.Constants.Constants;
    using static SuratBook.Data.Constants.ErrorMessages;

    public class GroupCreateFormModel
    {
        [Required]
        [StringLength(GroupNameMaxLength, MinimumLength = GroupNameMinLength, ErrorMessage = GroupNameErrorMessage)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(GroupInfoMaxLength, MinimumLength = GroupInfoMinLength, ErrorMessage = GroupInfoErrorMessage)]
        public string GroupInfo { get; set; } = null!;

        [Required]
        public int AccessId { get; set; }
    }
}
