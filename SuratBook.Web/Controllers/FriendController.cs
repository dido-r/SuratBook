namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IRequestServices services;

        public FriendController(IRequestServices _services)
        {
            services = _services;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllSuggestions()
        {
            var userId = GetUserId();
            var list = await services.GetAllSuggestionsAsync(userId);

            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddFriend([FromQuery] string friendId)
        {
            var userId = GetUserId();
            await services.SendRequestAsync(userId, friendId);
            return Ok();
        }

        [HttpPost]
        [Route("accept")]
        public async Task<IActionResult> AcceptFiendRequest([FromQuery] string userId)
        {
            var friendId = GetUserId();
            await services.AddFriendAsync(userId, friendId);
            return Ok();
        }

        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> GetSentRequests()
        {
            var userId = GetUserId();
            var sentRequests = await services.GetSentRequestAsync(userId);
            return Ok(sentRequests);
        }

        [HttpGet]
        [Route("invitations")]
        public async Task<IActionResult> GetFriendInvitations()
        {
            var userId = GetUserId();
            var invitations = await services.GetFriendInvitationsAsync(userId);
            return Ok(invitations);
        }

        [HttpGet]
        [Route("my-friends")]
        public async Task<IActionResult> GetFriends()
        {
            var userId = GetUserId();
            var friendList = await services.GetFriendsAsync(userId);
            return Ok(friendList);
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveFriend([FromQuery] string friendId)
        {
            var userId = GetUserId();
            await services.RemoveFriendAsync(userId, friendId);
            return Ok();
        }

        [HttpGet]
        [Route("check-friendship")]
        public async Task<IActionResult> CheckFriendship([FromQuery] string friendId)
        {
            var userId = GetUserId();
            var response = await services.CheckFriendship(userId, friendId);
            return Ok(response);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
