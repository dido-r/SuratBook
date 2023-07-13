using SuratBook.Services.Models;

namespace SuratBook.Services.Interfaces
{
    public interface IUserServices
    {
        Task<LoggedUserModel> RegiterUserAsync(RegisterUserModel model);

        Task<LoggedUserModel> LoginUserAsync(LoginUserModel model);

        Task<LoggedUserModel> GetCurrentUserAsync(string id);

        Task LogoutUserAsync();
    }
}
