using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.Post;

namespace SuratBook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostServices services;

        public PostController(IPostServices _services)
        {
            services = _services;
        }

        [HttpPost]
        [Route("create-post")]
        public async Task<IActionResult> CreatePost(CreatePostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await services.CreatePostAsync(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("delete-post")]
        public async Task<IActionResult> DeletePost(DeletePostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await services.DeletePostAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("edit-post")]
        public async Task<IActionResult> EditPost(EditPostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await services.EditPostAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("get-all-posts")]
        public async Task<IActionResult> GetAllPost()
        {
            var userId = GetUserId();
            var allPosts = await services.GetAllPostsAsync(userId);
            return Ok(allPosts);
        }

        [HttpGet]
        [Route("get-my-posts")]
        public async Task<IActionResult> GetMyPost([FromQuery] string id)
        {
            var userId = GetUserId();
            var myPosts = await services.GetMyPostAsync(id, userId);
            return Ok(myPosts);
        }

        [HttpPost]
        [Route("like")]
        public async Task<IActionResult> LikePost([FromQuery] string postId)
        {
            var userId = GetUserId();
            await services.LikePostAsync(userId, postId);
            return Ok();
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
