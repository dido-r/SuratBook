namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Chat;

    public interface IChatRoomServices
    {
        Task<ChatHistoryViewModel> CreateChatRoom(string senderId, string receiverId);

        Task<ChatHistoryViewModel> AddChatRoom(string chatId, string userId);

        Task<string> IsChatExisting(string senderId, string receiverId);

        Task<IEnumerable<ChatHistoryViewModel>> GetAllChatRoomsByUserIdAsync(string userId);

        Task CreateConnection(ChatRoomCreateConnectionModel model, string userId);

        Task<IEnumerable<string>> GetConnectionsByChatId(string chatId);

        Task CreateMessageAsync(ChatMessageCreateModel message);

        Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string chatId, int offset, int messageLimit);

        Task SetNotificationAsync(string chatId, string param);
    }
}
