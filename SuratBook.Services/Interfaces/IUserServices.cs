using SuratBook.Services.Models;

namespace SuratBook.Services.Interfaces
{
    public interface IUserServices
    {
        Task<string> RegiterUserAsync(RegisterUserModel model);

        Task<string> LoginUserAsync(LoginUserModel model);

        Task LogoutUserAsync();
    }
}
