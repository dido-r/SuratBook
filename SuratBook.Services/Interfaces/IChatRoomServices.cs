using SuratBook.Services.Models.Chat;

namespace SuratBook.Services.Interfaces
{
    public interface IChatRoomServices
    {
        Task CreateChatRoom(string senderId, string receiverId);

        Task<IEnumerable<ChatHistoryViewModel>> GetAllChatRoomsByUserIdAsync(string userId);

        Task CreateConnection(ChatRoomCreateConnectionModel model, string userId);

        Task<IEnumerable<string>> GetConnectionsByChatId(string chatId);
    }
}
