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

        Task LogoutUserAsync();

        Task<IEnumerable<LoggedUserModel>> SearchUsersByNameAsync(string name);

        Task<LoggedUserModel> GetUserNameAsync(string userId);

        void DeleteCookies(HttpResponse response);

        void GenerateCookie(LoggedUserModel user, HttpResponse response);

        Task<bool> IsAdmin(string userId);
    }
}
