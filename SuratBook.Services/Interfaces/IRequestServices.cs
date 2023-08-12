namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Friend;

    public interface IRequestServices
    {
        Task<IEnumerable<FriendViewModel>> GetAllSuggestionsAsync(string userId);

        Task SendRequestAsync(string userId, string friendId);

        Task RemoveRequestAsync(string userId, string friendId);

        Task AddFriendAsync(string userId, string friendId);

        Task<IEnumerable<FriendViewModel>> GetSentRequestAsync(string userId);

        Task<IEnumerable<FriendViewModel>> GetFriendInvitationsAsync(string userId);

        Task<IEnumerable<FriendViewModel>> GetFriendsAsync(string userId);

        Task RemoveFriendAsync(string userId, string friendId);

        Task<string> CheckFriendship(string userId, string friendId);
    }
}
