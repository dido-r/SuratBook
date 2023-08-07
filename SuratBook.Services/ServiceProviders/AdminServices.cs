using Microsoft.EntityFrameworkCore;
using SuratBook.Data;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.Admin;

namespace SuratBook.Services.ServiceProviders
{
    public class AdminServices : IAdminServices
    {
        private readonly SuratBookDbContext context;

        public AdminServices(SuratBookDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<PostAdminViewModel>> GetAllPostsAsync()
        {
            return await context
                .Posts
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new PostAdminViewModel
                {
                    Id = x.Id.ToString(),
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString("dd-MM-yyyy"),
                    Creator = x.Owner.Email,
                    Image = x.DropboxPath
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserAdminViewModel>> GetAllUsersAsync()
        {
            return await context
                .Users
                .Select(x => new UserAdminViewModel
                {
                    Id = x.Id.ToString(),
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.BirthDate.ToString("dd-MM-yyyy"),
                })
                .ToListAsync();
        }
    }
}
