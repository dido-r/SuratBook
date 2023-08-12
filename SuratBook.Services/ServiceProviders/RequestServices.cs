namespace SuratBook.Services.ServiceProviders
{
    using Microsoft.EntityFrameworkCore;

    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Friend;

    public class RequestServices : IRequestServices
    {

        private readonly SuratBookDbContext context;

        public RequestServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task SendRequestAsync(string userId, string friendId)
        {
            var requester = new FriendRequester
            {
                SuratUserId = Guid.Parse(userId)
            };

            var recipient = new FriendRecipient
            {
                SuratUserId = Guid.Parse(friendId)
            };

            await context.FriendsRequesters.AddAsync(requester);
            await context.FriendsRecipients.AddAsync(recipient);
            await context.SaveChangesAsync();

            var request = new FriendsRequests
            {
                RequesterId = requester.Id,
                RecipientId = recipient.Id,
            };

            await context.FriendsRequests.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRequestAsync(string userId, string friendId)
        {
            var request = await context
                .FriendsRequests
                .FirstAsync(x => x.Requester.SuratUserId.ToString() == userId && x.Recipient.SuratUserId.ToString() == friendId);
            var requster = await context.FriendsRequesters.FindAsync(request.RequesterId);
            var recipient = await context.FriendsRecipients.FindAsync(request.RecipientId);
            context.FriendsRecipients.Remove(recipient!);
            context.FriendsRequesters.Remove(requster!);
            context.FriendsRequests.Remove(request);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendViewModel>> GetAllSuggestionsAsync(string userId)
        {
            var admin = await context
                .UserRoles
                .FirstOrDefaultAsync();

            return await context
                .Users
                .Where(x => x.Id != admin!.UserId && x.Id.ToString() != userId && !x.ReceivedFriendsRequests.Any(z => z.SuratUserId == x.Id) && !x.SentFriendsRequests.Any(z => z.SuratUserId == x.Id))
                .Select(x => new FriendViewModel
                {
                    Id = x.Id.ToString(),
                    Name = $"{x.FirstName} {x.LastName}",
                }).ToListAsync();
        }

        public async Task<IEnumerable<FriendViewModel>> GetFriendInvitationsAsync(string userId)
        {
            return await context
                .FriendsRequests
                .Where(x => x.Recipient.SuratUserId.ToString() == userId)
                .Select(x => new FriendViewModel
                {
                    Id = x.Requester.SuratUserId.ToString(),
                    Name = $"{x.Requester.SuratUser.FirstName} {x.Requester.SuratUser.LastName}",
                }).ToListAsync();
        }

        public async Task<IEnumerable<FriendViewModel>> GetSentRequestAsync(string userId)
        {
            return await context
                .FriendsRequests
                .Where(x => x.Requester.SuratUserId.ToString() == userId && !x.AreFriends)
                .Select(x => new FriendViewModel
                {
                    Id = x.Recipient.SuratUserId.ToString(),
                    Name = $"{x.Recipient.SuratUser.FirstName} {x.Recipient.SuratUser.LastName}",
                }).ToListAsync();
        }

        public async Task AddFriendAsync(string userId, string friendId)
        {
            var request = await context
                .FriendsRequests
                .FirstAsync(x => x.Requester.SuratUserId.ToString() == userId && x.Recipient.SuratUserId.ToString() == friendId);
            request.AreFriends = true;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendViewModel>> GetFriendsAsync(string userId)
        {
            return await context
                .FriendsRequests
                .Where(x => (x.Recipient.SuratUserId.ToString() == userId || x.Requester.SuratUserId.ToString() == userId) && x.AreFriends)
                .Select(x => new FriendViewModel
                {
                    Id = x.Requester.SuratUserId.ToString() == userId ? x.Recipient.SuratUserId.ToString() : x.Requester.SuratUserId.ToString(),
                    Name = x.Requester.SuratUserId.ToString() == userId ? $"{x.Recipient.SuratUser.FirstName} {x.Recipient.SuratUser.LastName}" : $"{x.Requester.SuratUser.FirstName} {x.Requester.SuratUser.LastName}"
                }).ToListAsync();
        }

        public async Task RemoveFriendAsync(string userId, string friendId)
        {
            var request = await context.FriendsRequests.FirstAsync(x => (x.Requester.SuratUserId.ToString() == userId || x.Recipient.SuratUserId.ToString() == userId) && (x.Recipient.SuratUserId.ToString() == friendId || x.Requester.SuratUserId.ToString() == friendId));
            var requster = await context.FriendsRequesters.FindAsync(request.RequesterId);
            var recipient = await context.FriendsRecipients.FindAsync(request.RecipientId);
            context.FriendsRequests.Remove(request);
            context.FriendsRequesters.Remove(requster!);
            context.FriendsRecipients.Remove(recipient!);
            await context.SaveChangesAsync();
        }

        public async Task<string> CheckFriendship(string userId, string friendId)
        {
            var request = await context.FriendsRequests.FirstOrDefaultAsync(x => (x.Requester.SuratUserId.ToString() == userId || x.Recipient.SuratUserId.ToString() == userId) && (x.Recipient.SuratUserId.ToString() == friendId || x.Requester.SuratUserId.ToString() == friendId));

            if (request == null)
            {
                return "No friends";
            }

            if (!request.AreFriends)
            {
                return "Pending request";
            }

            return "Friends";
        }
    }
}
