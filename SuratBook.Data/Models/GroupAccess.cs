namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class GroupAccess
    {
        public int Id { get; set; }

        [Required]
        [StringLength(GroupAccessMaxLength, MinimumLength = GroupAccessMinLength)]
        public string Name { get; set; } = null!;
    }
}
