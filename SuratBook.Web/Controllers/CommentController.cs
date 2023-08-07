namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Comment;
    using SuratBook.Web.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices service;

        public CommentController(ICommentServices _service)
        {
            service = _service;
        }

        [HttpPost]
        [Route("comment-photo")]
        public async Task<IActionResult> CommentPhoto(CommentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            var userId = GetUserId();
            var comment = await service.CommentPhotoAsync(model, userId);
            return Ok(comment);
        }

        [HttpGet]
        [Route("get-photo-comments")]
        public async Task<IActionResult> GetCommentsByPhotoIdAsync([FromQuery] string photoId)
        {
            var comments = await service.GetPhotoCommentsAsync(photoId);
            return Ok(comments);
        }

        [HttpPost]
        [Route("comment-post")]
        public async Task<IActionResult> CommentPost(CommentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            var userId = GetUserId();
            var comment = await service.CommentPostAsync(model, userId);
            return Ok(comment);
        }

        [HttpGet]
        [Route("get-post-comments")]
        public async Task<IActionResult> GetPostComments([FromQuery] string postId)
        {
            var comments = await service.GetPostCommentsAsync(postId);
            return Ok(comments);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
