namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Group;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupServices services;

        public GroupController(IGroupServices _services)
        {
            services = _services;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGroup(GroupCreateFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var userId = GetUserId();
                await services.CreateGroupAsync(model, userId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("owner")]
        public async Task<IActionResult> GetOwnedGroups()
        {
            var userId = GetUserId();

            try
            {
                var groups = await services.GetOwnedGroupsAsync(userId);
                return Ok(groups);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var groups = await services.GetAllGroupsAsync();
                return Ok(groups);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetGroupPosts([FromQuery] string groupId)
        {
            try
            {
                var posts = await services.GetGroupPostsAsync(groupId);
                return Ok(posts);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("edit-info")]
        public async Task<IActionResult> EditGroupInfo(GroupInfoEditformModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await services.EditGroupInfoAsync(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("data")]
        public async Task<IActionResult> GetGroupData([FromQuery] string groupId)
        {
            try
            {
                var data = await services.GetGroupDataAsync(groupId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
