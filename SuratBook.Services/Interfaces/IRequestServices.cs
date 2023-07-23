namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Friend;

    public interface IRequestServices
    {
        Task<IEnumerable<FiendViewModel>> GetAllSuggestionsAsync(string userId);

        Task SendRequestAsync(string userId, string friendId);

        Task AddFriendAsync(string userId, string friendId);

        Task<IEnumerable<FiendViewModel>> GetSentRequestAsync(string userId);

        Task<IEnumerable<FiendViewModel>> GetFriendInvitationsAsync(string userId);

        IEnumerable<FiendViewModel> GetFriends(string userId);
    }
}
