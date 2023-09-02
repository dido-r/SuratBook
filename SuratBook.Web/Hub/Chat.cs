namespace SuratBook.Web.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using SuratBook.Services.Models.Chat;

    public class Chat : Hub
    {
        public async Task SendMessage(ChatMessageViewModel message, string[] connections)
        {
            await Clients.Clients(connections).SendAsync("ReceiveMessage", message);
        }

        public async Task SendNotification(string currentChatId, string connection)
        {
            await Clients.Client(connection).SendAsync("ReceiveNotification", currentChatId);
        }
    }
}
