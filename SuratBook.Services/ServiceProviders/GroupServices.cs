﻿using Microsoft.EntityFrameworkCore;
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
                .Groups.FindAsync(Guid.Parse(model.Id)) ?? throw new ArgumentNullException("Goup doesn't exist");

            group.Name = model.Name;
            group.GroupInfo = model.GroupInfo;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetGroupPostsAsync(string groupId)
        {
            var posts = await context
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

            return posts;
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
                    GroupInfo = x.GroupInfo
                })
                .ToListAsync();
        }

        public async Task<GroupViewModel> GetGroupDataAsync(string groupId)
        {
            return await context
                .Groups
                .Where(x => x.Id.ToString() == groupId)
                .Select(x => new GroupViewModel
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    GroupInfo = x.GroupInfo,
                    MainPhoto = x.MainPhoto!,
                    OwnerId = x.OwnerId.ToString(),
                }).FirstAsync();
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

            await context.AddAsync(join);
            await context.SaveChangesAsync();
        }

        public async Task LeaveGroupAsync(string groupId, string userId)
        {
            var pair = await context
                .UsersJoinedGroups
                .FirstOrDefaultAsync(x => x.SuratUserId.ToString() == userId && x.GrouptId.ToString() == groupId) ?? throw new ArgumentNullException();

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
    }
}
