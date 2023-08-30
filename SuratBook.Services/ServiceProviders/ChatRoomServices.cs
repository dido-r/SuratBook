using Microsoft.EntityFrameworkCore;
using SuratBook.Data;
using SuratBook.Data.Models;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.Chat;
using System.Threading.Tasks;

namespace SuratBook.Services.ServiceProviders
{
    public class ChatRoomServices : IChatRoomServices
    {
        private SuratBookDbContext context;

        public ChatRoomServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task CreateChatRoom(string senderId, string receiverId)
        {
            var chat = new ChatRoom();
            await context.ChatRooms.AddAsync(chat);
            await context.SaveChangesAsync();
            var sender = new ChatRoomParticipant
            {
                UserId = Guid.Parse(senderId),
                ChatRoomId = chat.Id,
            };
            var receiver = new ChatRoomParticipant
            {
                UserId = Guid.Parse(receiverId),
                ChatRoomId = chat.Id,
            };
            await context.ChatRoomParticipants.AddRangeAsync(sender, receiver);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatHistoryViewModel>> GetAllChatRoomsByUserIdAsync(string userId)
        {
            return await context
                .ChatRoomParticipants
                .Where(x => x.UserId.ToString() == userId)
                .Select(x => new ChatHistoryViewModel 
                { 
                    Id = x.ChatRoomId.ToString(),
                    ChatFriendName = x.ChatRoom.ChatRoomParticipants.First(u => u.UserId.ToString() != userId).SuratUser.FullName,
                    ChatFriendImage = x.ChatRoom.ChatRoomParticipants.First(u => u.UserId.ToString() != userId).SuratUser.MainPhoto,
                }).ToListAsync();
        }

        public async Task CreateConnection(ChatRoomCreateConnectionModel model, string userId)
        {
            var chatRoom = context
                .ChatRooms
                .Include(x => x.Connections)
                .FirstOrDefault(x => x.Id.ToString() == model.ChatRoomId);

            var connection = chatRoom!.Connections.Any(x => x.ConnectionId == model.ConnectionId);

            if (connection) 
            {
                return;
            }

            var currentConnection = await context
                .ChatConnections.FirstOrDefaultAsync(x => x.UserId == userId && x.ChatRoomId.ToString() == model.ChatRoomId);  
            
            if (currentConnection == null)
            {
                var newConnection = new ChatConnection
                {
                    ConnectionId = model.ConnectionId,
                    UserId = userId,
                    ChatRoomId = Guid.Parse(model.ChatRoomId)
                };

                await context.ChatConnections.AddAsync(newConnection);
                await context.SaveChangesAsync();
                return;
            }

            currentConnection!.ConnectionId = model.ConnectionId;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetConnectionsByChatId(string chatId)
        {
            return await context
                .ChatConnections
                .Where(x => x.ChatRoomId.ToString() == chatId)
                .Select(x => x.ConnectionId).ToListAsync();
        }

        public async Task CreateMessageAsync(ChatMessageCreateModel message)
        {
            var newMessage = new ChatMessage 
            {
                Message = message.Message,
                OwnerId = message.OwnerId,
                ChatRoomId = Guid.Parse(message.ChatRoomId)
            };

            await context.ChatMessages.AddAsync(newMessage);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string chatId)
        {
            return await context
                .ChatMessages
                .Where(x => x.ChatRoomId.ToString() == chatId)
                .Select(x => new ChatMessageViewModel
                { 
                    Message = x.Message,
                    UserId = x.OwnerId
                }).ToListAsync();
        }
    }
}
