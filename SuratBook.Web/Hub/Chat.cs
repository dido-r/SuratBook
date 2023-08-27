namespace SuratBook.Web.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using SuratBook.Web.Models;

    public class Chat : Hub
    {
        public async Task SendMessage(ChatMessageViewModel message, string[] connections)
        {
                await Clients.Clients(connections).SendAsync("ReceiveMessage", message);
        }
    }
}
