using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuratBook.Services.Interfaces;
using SuratBook.Services.Models.Post;
using SuratBook.Web.Models;

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
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            var result = await services.CreatePostAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete-post")]
        public async Task<IActionResult> DeletePost([FromQuery] string postId)
        {
            try
            {
                await services.DeletePostAsync(postId);
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
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
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
        public async Task<IActionResult> GetAllPost([FromQuery] int offset, int limit)
        {
            var userId = GetUserId();
            var allPosts = await services.GetAllPostsAsync(userId, offset, limit);
            return Ok(allPosts);
        }

        [HttpGet]
        [Route("get-my-posts")]
        public async Task<IActionResult> GetMyPost([FromQuery] string id, int offset, int limit)
        {
            var userId = GetUserId();
            var myPosts = await services.GetMyPostAsync(id, userId, offset, limit);
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

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchPosts([FromQuery] string name)
        {
            var userId = GetUserId();
            var posts = await services.SearchPostsAsync(name, userId);
            return Ok(posts);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
