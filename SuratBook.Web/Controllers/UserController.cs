﻿namespace WebApplication2.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.User;
    using SuratBook.Web.Models;

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
                return BadRequest();
            }

            try
            {
                var user = await service.LoginUserAsync(model);
                var response = Response;
                service.GenerateCookie(user, response);
                return Ok("Successful login");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
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
                return new ObjectResult(new ValidationError() { Message = $"{e.Message}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {
            var userId = GetUserId();
            var response = Response;
            service.DeleteCookies(response);
            await service.LogoutUserAsync(userId);
        }

        [HttpGet]
        [Authorize]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string userId)
        {
            try
            {
                var response = await service.GetUserInfoAsync(userId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return new ObjectResult(new ValidationError() { Message = $"{e.Message}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }

        [HttpPost]
        [Authorize]
        [Route("edit-info")]
        public async Task<IActionResult> EditUserInfo(UserInfoFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            var userId = GetUserId();
            await service.EditUserInfoAsync(model, userId);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("get-name")]
        public async Task<IActionResult> GetUserName([FromQuery] string userId)
        {
            try
            {
                var user = await service.GetUserNameAsync(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return new ObjectResult(new ValidationError() { Message = $"{e.Message}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

        }

        [HttpGet]
        [Authorize]
        [Route("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string name)
        {
            var users = await service.SearchUsersByNameAsync(name);
            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        [Route("is-admin")]
        public async Task<IActionResult> IsAdmin()
        {
            var userId = GetUserId();
            var result = await service.IsAdmin(userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = GetUserId();
            var result = await service.GetAllUsersAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("get-online-users")]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var userId = GetUserId();
            var result = await service.GetOnlineUsersAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("is-online")]
        public async Task<IActionResult> IsUserOnline()
        {
            var userId = GetUserId();
            var result = await service.IsOnline(userId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("set-online")]
        public async Task<IActionResult> SetUserOnline()
        {
            var userId = GetUserId();
            var result = await service.SetOnline(userId);
            return Ok(result);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
