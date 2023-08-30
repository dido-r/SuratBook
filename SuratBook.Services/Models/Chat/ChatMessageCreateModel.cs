namespace SuratBook.Services.Models.Chat
{
    public class ChatMessageCreateModel
    {
        public string Message { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public string ChatRoomId { get; set; } = null!;
    }
}
