namespace SuratBook.Services.ServiceProviders
{
    using Microsoft.EntityFrameworkCore;
    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Chat;

    public class ChatRoomServices : IChatRoomServices
    {
        private SuratBookDbContext context;

        public ChatRoomServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task<string> IsChatExisting(string senderId, string receiverId)
        {
            var isExist = await context
                .ChatRooms
                .Where(x => x.ChatRoomParticipants.Any(p => p.UserId.ToString() == senderId))
                .FirstOrDefaultAsync(x => x.ChatRoomParticipants.Any(p => p.UserId.ToString() == receiverId));

            return isExist != null ? isExist.Id.ToString() : "do not exist";
        }

        public async Task<ChatHistoryViewModel> CreateChatRoom(string senderId, string receiverId)
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

            var friend = await context
                .Users
                .FindAsync(Guid.Parse(receiverId));

            return new ChatHistoryViewModel
            {
                Id = chat.Id.ToString(),
                ChatFriendName = friend!.FullName,
                ChatFriendImage = friend.MainPhoto,
                Notification = chat.Notifications
            };
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
                    Notification = x.ChatRoom.Notifications
                }).ToListAsync();
        }

        public async Task CreateConnection(ChatRoomCreateConnectionModel model, string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            user!.ConnectionId = model.ConnectionId;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetConnectionsByChatId(string chatId)
        {
            return await context
                .ChatRoomParticipants
                .Where(x => x.ChatRoomId.ToString() == chatId)
                .Select(x => x.SuratUser.ConnectionId)
                .ToListAsync();
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

        public async Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string chatId, int offset, int messageLimit)
        {
            return await context
                .ChatMessages
                .Where(x => x.ChatRoomId.ToString() == chatId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(offset)
                .Take(messageLimit)
                .Select(x => new ChatMessageViewModel
                {
                    Message = x.Message,
                    UserId = x.OwnerId
                }).ToListAsync();
        }

        public async Task SetNotificationAsync(string chatId, string param)
        {
            var chat = await context
                .ChatRooms
                .FindAsync(Guid.Parse(chatId));

            chat!.Notifications = param == "on";
            await context.SaveChangesAsync();
        }
    }
}
