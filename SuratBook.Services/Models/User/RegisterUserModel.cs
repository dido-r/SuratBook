namespace SuratBook.Services.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class RegisterUserModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = FirstNameErrorMessage)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = LastNameErrorMessage)]
        public string LastName { get; set; } = null!;

        [Required]
        public string BirthDate { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Pass { get; set; } = null!;

        [Required]
        public string RePass { get; set; } = null!;
    }
}
