using Microsoft.AspNetCore.Http;
using SuratBook.Services.Models.User;

namespace SuratBook.Services.Interfaces
{
    public interface IUserServices
    {
        Task<LoggedUserModel> RegiterUserAsync(RegisterUserModel model);

        Task<LoggedUserModel> LoginUserAsync(LoginUserModel model);

        Task<LoggedUserModel> GetCurrentUserAsync(string id);

        Task LogoutUserAsync();

        void DeleteCookies(HttpResponse response);

        void GenerateCookie(LoggedUserModel user, HttpResponse response);
    }
}
