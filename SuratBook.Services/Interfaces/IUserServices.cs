namespace SuratBook.Services.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using SuratBook.Services.Models.User;

    public interface IUserServices
    {
        Task<LoggedUserModel> RegiterUserAsync(RegisterUserModel model);

        Task<LoggedUserModel> LoginUserAsync(LoginUserModel model);

        Task<UserInfoModel> GetUserInfoAsync(string userId);

        Task EditUserInfoAsync(UserInfoFormModel model, string userId);

        Task LogoutUserAsync(string userId);

        Task<IEnumerable<LoggedUserModel>> SearchUsersByNameAsync(string name);

        Task<IEnumerable<LoggedUserModel>> GetAllUsersAsync(string userId);

        Task<IEnumerable<LoggedUserModel>> GetOnlineUsersAsync(string userId);

        Task<LoggedUserModel> GetUserNameAsync(string userId);

        void DeleteCookies(HttpResponse response);

        void GenerateCookie(LoggedUserModel user, HttpResponse response);

        Task<bool> IsAdmin(string userId);

        Task<bool> IsOnline(string userId);

        Task<bool> SetOnline(string userId);
    }
}
