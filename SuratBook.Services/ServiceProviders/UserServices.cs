﻿namespace SuratBook.Services.ServiceProviders
{
    using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.User;

    public class UserServices : IUserServices
    {
        private SignInManager<SuratUser> signInManager;
        private readonly UserManager<SuratUser> userManager;
        private readonly IUserStore<SuratUser> userStore;
        private readonly IConfiguration configuration;
        private readonly SuratBookDbContext context;

        public UserServices(SignInManager<SuratUser> _signInManager,
            UserManager<SuratUser> _userManager,
            IUserStore<SuratUser> _userStore,
            IConfiguration _configuration,
            SuratBookDbContext _context)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            configuration = _configuration;
            context = _context;
        }

        public async Task<LoggedUserModel> LoginUserAsync(LoginUserModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email) ?? throw new ArgumentNullException("No user");
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Pass, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new ArgumentNullException("Invalid credentials");
            }

            var jwt = GenerateJWT(user);

            return new LoggedUserModel
            {
                Id = user.Id.ToString(),
                Name = $"{user.FirstName} {user.LastName}",
                Token = jwt,
            };
        }

        public async Task<LoggedUserModel> RegiterUserAsync(RegisterUserModel model)
        {
            var suratUser = new SuratUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDate = DateTime.Parse(model.BirthDate)
            };

            await userStore.SetUserNameAsync(suratUser, model.Email, CancellationToken.None);
            var result = await userManager.CreateAsync(suratUser, model.Pass);

            if (!result.Succeeded)
            {
                throw new ArgumentException($"{result.Errors.First().Description}");
            }

            var jwt = GenerateJWT(suratUser);
            await signInManager.SignInAsync(suratUser, isPersistent: false);

            return new LoggedUserModel
            {
                Id = suratUser.Id.ToString(),
                Name = $"{suratUser.FirstName} {suratUser.LastName}",
                Token = jwt,
            };
        }

        public async Task LogoutUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            user.Online = false;
            await context.SaveChangesAsync();
            await signInManager.SignOutAsync();
        }

        public async Task<UserInfoModel> GetUserInfoAsync(string userId)
        {
            var userInfo = await context
                .Users
                .Where(x => x.Id.ToString() == userId)
                .Select(x => new UserInfoModel
                {
                    Country = x.LocationId.HasValue ? x.Location!.Country : null,
                    Town = x.LocationId.HasValue ? x.Location!.Town : null,
                    Address = x.LocationId.HasValue ? x.Location!.Address : null,
                    University = x.EducationId.HasValue ? x.Education!.University : null,
                    UniversityDegreeId = x.EducationId.HasValue ? x.Education!.UniversityDegreeId : null,
                    UniversityDegree = x.EducationId.HasValue ? x.Education!.UniversityDegree.Name : null,
                    School = x.EducationId.HasValue ? x.Education!.School : null
                })
                .FirstOrDefaultAsync() ?? throw new Exception("No user found");

            return userInfo!;
        }

        public async Task EditUserInfoAsync(UserInfoFormModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user.LocationId == null)
            {

                var location = new Location
                {
                    Town = model.Town,
                    Address = model.Address,
                    Country = model.Country,
                };

                await context.Locations.AddAsync(location);
                await context.SaveChangesAsync();
                user.LocationId = location.Id;
            }
            else
            {
                var location = await context.Locations.FindAsync(user.LocationId);
                location.Address = model.Address;
                location.Country = model.Country;
                location.Town = model.Town;
            }

            if (user.EducationId == null)
            {

                var education = new Education
                {
                    University = model.University,
                    UniversityDegreeId = model.UniversityDegreeId != 0 ? model.UniversityDegreeId : null,
                    School = model.School
                };

                await context.Educations.AddAsync(education);
                await context.SaveChangesAsync();
                user.EducationId = education.Id;
            }
            else
            {
                var education = await context.Educations.FindAsync(user.EducationId);
                education.University = model.University;
                education.UniversityDegreeId = model.UniversityDegreeId != 0 ? model.UniversityDegreeId : null;
                education.School = model.School;
            }

            await context.SaveChangesAsync();
        }

        public async Task<LoggedUserModel> GetUserNameAsync(string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId)) ?? throw new Exception("No user found");

            return new LoggedUserModel
            {
                Id = user!.Id.ToString(),
                Name = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task<bool> IsAdmin(string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            return await userManager.IsInRoleAsync(user!, "Admin");
        }

        public async Task<IEnumerable<LoggedUserModel>> SearchUsersByNameAsync(string name)
        {
            return await context
                .Users
                .Where(x => name.Contains(x.FirstName) || name.Contains(x.LastName) || x.FirstName.Contains(name) || x.LastName.Contains(name))
                .Select(x => new LoggedUserModel
                {
                    Id = x.Id.ToString(),
                    Name = $"{x.FirstName} {x.LastName}"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<LoggedUserModel>> GetAllUsersAsync(string userId)
        {
            return await context
                .Users
                .Where(x => x.Id.ToString() != userId)
                .Select(x => new LoggedUserModel
                {
                    Id = x.Id.ToString(),
                    Name = x.FullName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<LoggedUserModel>> GetOnlineUsersAsync(string userId)
        {
            return await context
                .Users
                .Where(x => x.Id.ToString() != userId && x.Online)
                .Select(x => new LoggedUserModel
                {
                    Id = x.Id.ToString(),
                    Name = x.FullName
                })
                .ToListAsync();
        }

        public async Task<bool> IsOnline(string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            return user.Online;
        }

        public async Task<bool> SetOnline(string userId)
        {
            var user = await context
                .Users
                .FindAsync(Guid.Parse(userId));

            user.Online = true;
            await context.SaveChangesAsync();

            return user.Online;
        }

        public void GenerateCookie(LoggedUserModel user, HttpResponse response)
        {
            var cookieOptions = new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            };

            response.Cookies.Append("surat_auth", user.Id, cookieOptions);
            response.Cookies.Append("surat_name", user.Name, cookieOptions);
            response.Cookies.Append("surat_token", user.Token, cookieOptions);
        }

        public void DeleteCookies(HttpResponse response)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            };

            var jwtOptions = new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.None,
            };

            response.Cookies.Delete("surat_auth", cookieOptions);
            response.Cookies.Delete("surat_name", cookieOptions);
            response.Cookies.Delete("surat_token", jwtOptions);
        }

        private string GenerateJWT(SuratUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var claims = new[]
            {
                new Claim("iss", configuration["Jwt:Issuer"]),
                new Claim("aud", configuration["Jwt:Audience"]),
                new Claim("UserId", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}