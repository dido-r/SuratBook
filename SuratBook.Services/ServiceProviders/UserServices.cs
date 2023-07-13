using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuratBook.Data.Models;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuratBook.Services.ServiceProviders
{
    public class UserServices : IUserServices
    {
        private SignInManager<SuratUser> signInManager;
        private readonly UserManager<SuratUser> userManager;
        private readonly IUserStore<SuratUser> userStore;
        private readonly IConfiguration configuration;

        public UserServices(SignInManager<SuratUser> _signInManager,
            UserManager<SuratUser> _userManager,
            IUserStore<SuratUser> _userStore,
            IConfiguration _configuration)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            userStore = _userStore;
            configuration = _configuration;
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
                throw new ArgumentNullException("Invalid credentials");
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

        public async Task<LoggedUserModel> GetCurrentUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return new LoggedUserModel
            {
                Id = user.Id.ToString(),
                Name = $"{user.FirstName} {user.LastName}"
            };
        }

        public async Task LogoutUserAsync()
        {
            await signInManager.SignOutAsync();
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
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}