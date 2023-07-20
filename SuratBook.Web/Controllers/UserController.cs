using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.User;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices service;

        public UserController(IUserServices _service)
        {
            service = _service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credentials");
            }

            try
            {
                var user = await service.LoginUserAsync(model);
                var response = Response;
                service.GenerateCookie(user, response);
                return Ok("Successful login");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credentials");
            }

            try
            {
                var user = await service.RegiterUserAsync(model);
                var response = Response;
                service.GenerateCookie(user, response);
                return Ok("Successful register");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {
            var response = Response;
            service.DeleteCookies(response);
            await service.LogoutUserAsync();
        }

        [HttpGet]
        [Authorize]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string userId)
        {
            var response = await service.GetUserInfoAsync(userId);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [Route("edit-info")]
        public async Task<IActionResult> EditUserInfo(UserInfoFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await service.EditUserInfoAsync(model);
            return Ok();
        }
    }
}
