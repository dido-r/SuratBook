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
        public IActionResult GetFriends()
        {
            var userId = GetUserId();
            //TO DO
            return Ok();
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
