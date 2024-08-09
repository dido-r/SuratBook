namespace SuratBook.Services.Models.Chat
{
    public class ChatMessageViewModel
    {
        public string UserId { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string ChatRoomId { get; set; } = null!;
    }
}
