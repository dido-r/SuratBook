namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices service;

        public AdminController(IAdminServices _service)
        {
            service = _service;
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("all-posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await service.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}
