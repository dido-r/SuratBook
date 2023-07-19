namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Photo;

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
                return BadRequest();
            }

            try
            {
                var result = await service.CreatePhotoAsync(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-photos")]
        public async Task<IActionResult> GetPhoto([FromQuery] string id)
        {
            try
            {
                var result = await service.GetPhotosAsync(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("exist")]
        public async Task<IActionResult> FindByPathAsync([FromQuery] string path)
        {
            try
            {
                var result = await service.FindByPathAsync(path);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
