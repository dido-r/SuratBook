using System.ComponentModel.DataAnnotations;

namespace SuratBook.Services.Models.User
{
    public class LoginUserModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9][a-zA-Z0-9_.+-]+[a-zA-Z0-9]@[a-zA-Z0-9]*[a-zA-Z0-9-]+[a-zA-Z0-9].[a-zA-Z0-9-.]+[a-zA-Z0-9]$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 symbols")]
        public string Pass { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
