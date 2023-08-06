namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Comment;
    using SuratBook.Services.Models.Photo;
    using SuratBook.Web.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoServices service;

        public PhotoController(IPhotoServices _service)
        {
            service = _service;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> CreatePhoto(CreatePhotoModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = "Could not upload the photo" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            var result = await service.CreatePhotoAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-photos")]
        public async Task<IActionResult> GetPhoto([FromQuery] string id)
        {
            var userId = GetUserId();
            var result = await service.GetPhotosAsync(id, userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("exist")]
        public async Task<IActionResult> FindByPathAsync([FromQuery] string path)
        {
            var result = await service.FindByPathAsync(path);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("delete")]
        public async Task<IActionResult> DeletePhotoAsync([FromBody] string id)
        {
            try
            {
                await service.DeletePhotoAsync(id);
                return Ok();
            }
            catch
            {
                return new ObjectResult(new ValidationError() { Message = "Could not delete the photo" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("like")]
        public async Task<IActionResult> LikePhoto([FromBody] string photoId)
        {
            var userId = GetUserId();
            await service.LikePhotoAsync(photoId, userId);
            return Ok();
        }

        [HttpPost]
        [Route("comment")]
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
        [Route("get-comments")]
        public async Task<IActionResult> GetCommentsByPhotoIdAsync([FromQuery] string photoId)
        {
            var comments = await service.GetPhotoCommentsAsync(photoId);
            return Ok(comments);
        }

        [HttpPost]
        [Route("set-as-profile")]
        public async Task SetProfilePicture([FromBody] string paht)
        {
            var userId = GetUserId();
            await service.SetAsProfileAsync(userId, paht);
        }

        [HttpGet]
        [Route("get-a-profile")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userId = GetUserId();
            var result = await service.GetProfileImageAsync(userId);
            return Ok(result);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
