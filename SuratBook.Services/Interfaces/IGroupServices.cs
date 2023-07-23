using SuratBook.Services.Models.Group;
using SuratBook.Services.Models.Post;

namespace SuratBook.Services.Interfaces
{
    public interface IGroupServices
    {
        Task CreateGroupAsync(GroupCreateFormModel model, string userId);

        Task<IEnumerable<GroupViewModel>> GetOwnedGroupsAsync(string userId);

        Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync();

        Task<IEnumerable<PostViewModel>> GetGroupPostsAsync(string groupId);

        Task<bool> IsMember(string groupId, string userId);

        Task JoinGroupAsync(string groupId, string userId);

        Task LeaveGroupAsync(string groupId, string userId);

        Task<IEnumerable<GroupViewModel>> GetJoinedGroupAsync(string userId);

        Task<IEnumerable<GroupMembersViewModel>> GetGroupMembers(string groupId);

        Task EditGroupInfoAsync(GroupInfoEditformModel model);

        Task<GroupViewModel> GetGroupDataAsync(string groupId);

        Task<IEnumerable<GroupMediaViewModel>> GetGroupMediaFilesAsync(string groupId);
    }
}
