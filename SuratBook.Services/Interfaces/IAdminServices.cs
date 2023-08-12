namespace SuratBook.Services.Interfaces
{
    using SuratBook.Services.Models.Admin;

    public interface IAdminServices
    {
        Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync();

        Task<IEnumerable<PostAdminViewModel>> GetAllPostsAsync();

        Task<IEnumerable<GroupAdminViewModel>> GetAllGroupsAsync();

        Task ActivateGroupAsync(string groupId);
    }
}
