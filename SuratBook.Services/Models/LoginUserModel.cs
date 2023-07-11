using System.ComponentModel.DataAnnotations;

namespace SuratBook.Services.Models
{
    public class LoginUserModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9][a-zA-Z0-9_.+-]+[a-zA-Z0-9]@[a-zA-Z0-9]*[a-zA-Z0-9-]+[a-zA-Z0-9].[a-zA-Z0-9-.]+[a-zA-Z0-9]$")]
        public string Email { get; set; } = null!;

        [Required]
        public string Pass { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
