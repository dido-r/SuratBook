namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Group;
    using SuratBook.Services.Models.Post;

    public interface IGroupServices
    {
        Task CreateGroupAsync(GroupCreateFormModel model, string userId);

        Task<IEnumerable<GroupViewModel>> GetOwnedGroupsAsync(string userId);

        Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync();

        Task DeleteGroupAsync(string groupId);

        Task<IEnumerable<PostViewModel>> GetGroupPostsAsync(string groupId, string userId);

        Task<bool> IsMember(string groupId, string userId);

        Task JoinGroupAsync(string groupId, string userId);

        Task JoinPrivateGroupAsync(string groupId, string userId);

        Task ApproveJoinPrivateGroupAsync(string requestId);

        Task DeclineJoinPrivateGroupAsync(string requestId);

        Task<bool> IsPendingJoinRequestsAsync(string groupId, string userId);

        Task<IEnumerable<RequestUserViewModel>> GetPendingJoinRequestsAsync(string groupId);

        Task LeaveGroupAsync(string groupId, string userId);

        Task<IEnumerable<GroupViewModel>> GetJoinedGroupAsync(string userId);

        Task<IEnumerable<GroupMembersViewModel>> GetGroupMembers(string groupId);

        Task EditGroupInfoAsync(GroupInfoEditformModel model);

        Task<GroupViewModel> GetGroupDataAsync(string groupId);

        Task<IEnumerable<GroupMediaViewModel>> GetGroupMediaFilesAsync(string groupId);

        Task<IEnumerable<GroupViewModel>> SearchGroupsByNameAsync(string name);
    }
}
