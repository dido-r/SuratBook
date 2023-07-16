using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.User;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        private IUserServices service;

        public UserController(IUserServices _service)
        {
            service = _service;
        }

        [HttpGet]
        [Route("currentUser")]
        public async Task<LoggedUserModel> CurrentUser()
        {
            var id = Request.Cookies["surat_auth"] ?? throw new ArgumentNullException("Invalid user id");

            try
            {
                var reuslt = await service.GetCurrentUserAsync(id);
                return reuslt;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return "test";
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
    }
}
