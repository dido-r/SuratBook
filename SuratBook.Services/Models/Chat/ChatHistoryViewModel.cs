namespace SuratBook.Services.Models.Chat
{
    public class ChatHistoryViewModel
    {
        public string Id { get; set; } = null!;

        public string ChatFriendName { get; set; } = null!;

        public string? ChatFriendImage { get; set; }

        public string? LastMessage { get; set; }
    }
}