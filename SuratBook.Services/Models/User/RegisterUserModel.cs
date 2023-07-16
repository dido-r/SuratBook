namespace SuratBook.Services.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class RegisterUserModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public string BirthDate { get; set; } = null!;

        [Required]
        [RegularExpression("^[a-zA-Z0-9][a-zA-Z0-9_.+-]+[a-zA-Z0-9]@[a-zA-Z0-9]*[a-zA-Z0-9-]+[a-zA-Z0-9].[a-zA-Z0-9-.]+[a-zA-Z0-9]$")]
        public string Email { get; set; } = null!;

        [Required]
        public string Pass { get; set; } = null!;

        [Required]
        public string RePass { get; set; } = null!;
    }
}
