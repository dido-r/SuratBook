using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        private IUserServices service;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(IUserServices _service, IHttpContextAccessor _httpContextAccessor)
        {
            service = _service;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        [Route("api/currentUser")]
        public string CurrentUser()
        {
            return "TEST";
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<string> Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid data");
            }

            return await service.LoginUserAsync(model);
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<string> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid data");
            }

            return await service.RegiterUserAsync(model);
        }

        [HttpPost]
        [Route("api/logout")]
        public async Task Logout()
        {
            await service.LogoutUserAsync();
        }
    }
}
