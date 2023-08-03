namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Group;
    using SuratBook.Web.Models;

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

        public GroupController()
        {
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGroup(GroupCreateFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ModelState.Values.First().Errors.First().ErrorMessage}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
            var userId = GetUserId();
            await services.CreateGroupAsync(model, userId);
            return Ok();
        }

        [HttpGet]
        [Route("owner")]
        public async Task<IActionResult> GetOwnedGroups([FromQuery] string userId)
        {
            var groups = await services.GetOwnedGroupsAsync(userId);
            return Ok(groups);
        }

        [HttpGet]
        [Route("joined")]
        public async Task<IActionResult> GetJoinedGroups([FromQuery] string userId)
        {
            var groups = await services.GetJoinedGroupAsync(userId);
            return Ok(groups);
        }

        [HttpGet]
        [Route("get-members")]
        public async Task<IActionResult> GetGroupMember([FromQuery] string groupId)
        {
            var members = await services.GetGroupMembers(groupId);
            return Ok(members);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await services.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetGroupPosts([FromQuery] string groupId)
        {
            var posts = await services.GetGroupPostsAsync(groupId);
            return Ok(posts);
        }

        [HttpPost]
        [Route("edit-info")]
        public async Task<IActionResult> EditGroupInfo(GroupInfoEditformModel model)
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
                await services.EditGroupInfoAsync(model);
                return Ok();

            }
            catch (Exception ex)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ex.Message}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }

        [HttpGet]
        [Route("data")]
        public async Task<IActionResult> GetGroupData([FromQuery] string groupId)
        {
            var data = await services.GetGroupDataAsync(groupId);
            return Ok(data);
        }

        [HttpGet]
        [Route("membership")]
        public async Task<IActionResult> GroupMembershipCheck([FromQuery] string groupId)
        {
            var userId = GetUserId();
            var data = await services.IsMember(groupId, userId);
            return Ok(data);
        }

        [HttpPost]
        [Route("join")]
        public async Task<IActionResult> JoinGroup([FromQuery] string groupId)
        {
            var userId = GetUserId();
            await services.JoinGroupAsync(groupId, userId);
            return Ok();
        }

        [HttpPost]
        [Route("join-private")]
        public async Task<IActionResult> JoinPrivateGroup([FromQuery] string groupId)
        {
            var userId = GetUserId();
            await services.JoinPrivateGroupAsync(groupId, userId);
            return Ok();
        }

        [HttpGet]
        [Route("membership-pending")]
        public async Task<IActionResult> IsPendingJoinRequests([FromQuery] string groupId)
        {
            var userId = GetUserId();
            var response = await services.IsPendingJoinRequestsAsync(groupId, userId);
            return Ok(response);
        }

        [HttpGet]
        [Route("pending-requests")]
        public async Task<IActionResult> GetPendingJoinRequests([FromQuery] string groupId)
        {
            var response = await services.GetPendingJoinRequestsAsync(groupId);
            return Ok(response);
        }

        [HttpPost]
        [Route("approve-request")]
        public async Task<IActionResult> ApprovePendingJoinRequests([FromQuery] string requestId)
        {
            await services.ApproveJoinPrivateGroupAsync(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("decline-request")]
        public async Task<IActionResult> DeclinePendingJoinRequests([FromQuery] string requestId)
        {
            await services.DeclineJoinPrivateGroupAsync(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("leave")]
        public async Task<IActionResult> LeaveGroup([FromQuery] string groupId)
        {
            var userId = GetUserId();

            try
            {
                await services.LeaveGroupAsync(groupId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ValidationError() { Message = $"{ex.Message}" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }

        [HttpGet]
        [Route("get-media")]
        public async Task<IActionResult> GetGroupMediaFiles([FromQuery] string id)
        {
            var files = await services.GetGroupMediaFilesAsync(id);
            return Ok(files);
        }

        [HttpGet]
        [Authorize]
        [Route("search")]
        public async Task<IActionResult> SearchGroups([FromQuery] string name)
        {
            var groups = await services.SearchGroupsByNameAsync(name);
            return Ok(groups);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
