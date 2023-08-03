using Microsoft.EntityFrameworkCore;
using SuratBook.Data;
using SuratBook.Data.Models;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.Group;
using SuratBook.Services.Models.Post;

namespace SuratBook.Services.ServiceProviders
{
    public class GroupServices : IGroupServices
    {
        private readonly SuratBookDbContext context;

        public GroupServices()
        {

        }

        public GroupServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task CreateGroupAsync(GroupCreateFormModel model, string userId)
        {

            var group = new Group
            {
                Name = model.Name,
                GroupInfo = model.GroupInfo,
                AccessId = model.AccessId,
                OwnerId = Guid.Parse(userId)
            };

            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
        }

        public async Task EditGroupInfoAsync(GroupInfoEditformModel model)
        {
            var group = await context
                .Groups.FindAsync(Guid.Parse(model.Id)) ?? throw new Exception("Group doesn't exist");

            group.Name = model.Name;
            group.GroupInfo = model.GroupInfo;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetGroupPostsAsync(string groupId)
        {
            return await context
                .Posts
                .Where(x => x.GroupId.ToString() == groupId)
                .Select(x => new PostViewModel
                {
                    Key = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath,
                    OwnerId = x.OwnerId.ToString(),
                    OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                    Likes = x.Likes,
                    Comments = x.Comments.Count()
                }).ToListAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetOwnedGroupsAsync(string userId)
        {
            return await context
                .Groups
                .Where(x => x.OwnerId.ToString() == userId)
                .Select(x => new GroupViewModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    MainPhoto = x.MainPhoto!,
                    GroupInfo = x.GroupInfo,
                    OwnerId = x.OwnerId.ToString(),
                    Access = x.Access.Name
                })
                .ToListAsync();
        }

        public async Task<GroupViewModel> GetGroupDataAsync(string groupId)
        {
            var group = await context
                .Groups
                .Include(x => x.Access)
                .FirstOrDefaultAsync(x => x.Id.ToString() == groupId);

            return new GroupViewModel
            {
                Id = group.Id.ToString(),
                Name = group.Name,
                GroupInfo = group.GroupInfo,
                MainPhoto = group.MainPhoto!,
                OwnerId = group.OwnerId.ToString(),
                Access = group.Access.Name
            };
        }

        public async Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync()
        {
            return await context
               .Groups
               .Select(x => new GroupViewModel
               {
                   Id = x.Id.ToString(),
                   Name = x.Name,
                   MainPhoto = x.MainPhoto!,
                   GroupInfo = x.GroupInfo,
                   OwnerId = x.OwnerId.ToString(),
                   Access = x.Access.Name
               })
               .ToListAsync();
        }

        public async Task<bool> IsMember(string groupId, string userId)
        {
            return await context
                .UsersJoinedGroups
                .AnyAsync(x => x.SuratUserId.ToString() == userId && x.GrouptId.ToString() == groupId);
        }

        public async Task JoinGroupAsync(string groupId, string userId)
        {
            var join = new UsersJoinedGroups
            {
                GrouptId = Guid.Parse(groupId),
                SuratUserId = Guid.Parse(userId)
            };

            await context.UsersJoinedGroups.AddAsync(join);
            await context.SaveChangesAsync();
        }

        public async Task JoinPrivateGroupAsync(string groupId, string userId)
        {
            var request = new GroupJoinRequest
            {
                SuratUserId = Guid.Parse(userId),
                GrouptId = Guid.Parse(groupId)
            };

            await context.GroupsJoinRequests.AddRangeAsync(request);
            await context.SaveChangesAsync();
        }

        public async Task LeaveGroupAsync(string groupId, string userId)
        {
            var pair = await context
                .UsersJoinedGroups
                .FirstOrDefaultAsync(x => x.SuratUserId.ToString() == userId && x.GrouptId.ToString() == groupId) ?? throw new Exception("Group doesn't exist");

            context.Remove(pair);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetJoinedGroupAsync(string userId)
        {
            return await context
                .UsersJoinedGroups
                .Where(x => x.SuratUserId.ToString() == userId)
                .Select(x => new GroupViewModel
                {
                    Id = x.GrouptId.ToString(),
                    Name = x.Group.Name,
                    GroupInfo = x.Group.GroupInfo,
                    MainPhoto = x.Group.MainPhoto!,
                    OwnerId = x.Group.OwnerId.ToString(),
                    Access = x.Group.Access.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GroupMembersViewModel>> GetGroupMembers(string groupId)
        {
            return await context
            .UsersJoinedGroups
            .Where(x => x.GrouptId.ToString() == groupId)
            .Select(x => new GroupMembersViewModel
            {
                Id = x.SuratUserId.ToString(),
                Name = $"{x.SuratUser.FirstName} {x.SuratUser.LastName}",
                DropboxPath = x.SuratUser.MainPhoto!
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<GroupMediaViewModel>> GetGroupMediaFilesAsync(string groupId)
        {
            return await context
                .Posts
                .Where(x => x.GroupId.ToString() == groupId)
                .Select(x => new GroupMediaViewModel
                {
                    Id = x.Id.ToString(),
                    Description = x.Description,
                    DropboxPath = x.DropboxPath!,
                    Likes = x.Likes,
                    Comments = x.Comments.Count()
                }).ToListAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> SearchGroupsByNameAsync(string name)
        {
            return await context
                .Groups
                .Where(x => name.Contains(x.Name) || x.Name.Contains(name))
                .Select(x => new GroupViewModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    GroupInfo = x.GroupInfo,
                    MainPhoto = x.MainPhoto,
                    OwnerId = x.OwnerId.ToString(),
                    Access = x.Access.Name
                })
                .ToListAsync();
        }

        public async Task<bool> IsPendingJoinRequestsAsync(string groupId, string userId)
        {
            return await context
                .GroupsJoinRequests
                .AnyAsync(x => x.SuratUserId.ToString() == userId && x.GrouptId.ToString() == groupId);
        }

        public async Task<IEnumerable<RequestUserViewModel>> GetPendingJoinRequestsAsync(string groupId)
        {
            return await context
                .GroupsJoinRequests
                .Where(x => x.GrouptId.ToString() == groupId)
                .Select(x => new RequestUserViewModel
                {
                    Id = x.Id.ToString(),
                    UserId = x.SuratUserId.ToString(),
                    Name = $"{x.SuratUser.FirstName} {x.SuratUser.LastName}",
                    DropboxPath = x.SuratUser.MainPhoto
                }).ToListAsync();
        }

        public async Task ApproveJoinPrivateGroupAsync(string requestId)
        {
            var request = await context
                .GroupsJoinRequests
                .FindAsync(Guid.Parse(requestId));

            var join = new UsersJoinedGroups
            {
                GrouptId = request!.GrouptId,
                SuratUserId = request.SuratUserId
            };

            context.GroupsJoinRequests.Remove(request);
            await context.UsersJoinedGroups.AddAsync(join);
            await context.SaveChangesAsync();
        }

        public async Task DeclineJoinPrivateGroupAsync(string requestId)
        {
            var request = await context
                .GroupsJoinRequests
                .FindAsync(Guid.Parse(requestId));

            context.GroupsJoinRequests.Remove(request);
            await context.SaveChangesAsync();
        }
    }
}
