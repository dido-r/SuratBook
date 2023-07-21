using Microsoft.AspNetCore.Http;
using SuratBook.Services.Models.User;

namespace SuratBook.Services.Interfaces
{
    public interface IUserServices
    {
        Task<LoggedUserModel> RegiterUserAsync(RegisterUserModel model);

        Task<LoggedUserModel> LoginUserAsync(LoginUserModel model);

        Task<UserInfoModel> GetUserInfoAsync(string userId);

        Task EditUserInfoAsync(UserInfoFormModel model);

        Task LogoutUserAsync();

        Task<LoggedUserModel> GetUserNameAsync(string userId);

        void DeleteCookies(HttpResponse response);

        void GenerateCookie(LoggedUserModel user, HttpResponse response);
    }
}
