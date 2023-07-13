using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    [ApiController]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        private IUserServices service;

        public UserController(IUserServices _service)
        {
            service = _service;
        }

        [HttpGet]
        [Route("api/currentUser")]
        public async Task<LoggedUserModel> CurrentUser()
        {
            var id = Request.Cookies["surat_auth"];


            if (id == null)
            {
                throw new ArgumentNullException();
            }

            return await service.GetCurrentUserAsync(id);
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid data!");
            }

            try
            {
                var user = await service.LoginUserAsync(model);
                GenerateCookie(user);
                return Ok("Successful login");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid data!");
            }

            try
            {
                var user = await service.RegiterUserAsync(model);
                GenerateCookie(user);
                return Ok("Successful register");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/logout")]
        public async Task Logout()
        {
            DeleteCookies();
            await service.LogoutUserAsync();
        }

        private void GenerateCookie(LoggedUserModel user)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            };

            var jwtOptions = new CookieOptions()
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            };

            Response.Cookies.Append("surat_auth", user.Id, cookieOptions);
            Response.Cookies.Append("surat_name", user.Name, cookieOptions);
            Response.Cookies.Append("surat_token", user.Token, jwtOptions);
        }

        private void DeleteCookies()
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

            Response.Cookies.Delete("surat_auth", cookieOptions);
            Response.Cookies.Delete("surat_name", cookieOptions);
            Response.Cookies.Delete("surat_token", jwtOptions);
        }
    }
}
