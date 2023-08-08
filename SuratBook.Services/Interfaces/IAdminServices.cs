using SuratBook.Services.Models.Admin;

namespace SuratBook.Services.Interfaces
{
    public interface IAdminServices
    {
        Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync();

        Task<IEnumerable<PostAdminViewModel>> GetAllPostsAsync();

        Task<IEnumerable<GroupAdminViewModel>> GetAllGroupsAsync();

        Task ActivateGroupAsync(string groupId);
    }
}
