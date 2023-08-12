namespace SuratBook.Services.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 symbols")]
        public string Pass { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
