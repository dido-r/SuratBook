namespace SuratBook.Web.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using SuratBook.Web.Models;

    public class Chat : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
