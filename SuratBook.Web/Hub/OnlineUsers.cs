namespace SuratBook.Web.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using SuratBook.Services.Models.User;

    public class OnlineUsers : Hub
    {
        public async Task SetOnline(LoggedUserModel user, string connection)
        {
            await Clients.AllExcept(connection).SendAsync("Online", user);
        }

        public async Task SetOffline(string userId)
        {
            await Clients.All.SendAsync("Offline", userId);
        }
    }
}
