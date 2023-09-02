using SuratBook.Services.Models.Chat;

namespace SuratBook.Services.Interfaces
{
    public interface IChatRoomServices
    {
        Task CreateChatRoom(string senderId, string receiverId);

        Task<IEnumerable<ChatHistoryViewModel>> GetAllChatRoomsByUserIdAsync(string userId);

        Task CreateConnection(ChatRoomCreateConnectionModel model, string userId);

        Task<IEnumerable<string>> GetConnectionsByChatId(string chatId);

        Task CreateMessageAsync(ChatMessageCreateModel message);

        Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string chatId, int offset, int messageLimit);

        Task SetNotificationAsync(string chatId, string param);
    }
}
