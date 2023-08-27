namespace SuratBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SuratBook.Services.Interfaces;
    using SuratBook.Services.Models.Chat;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomServices services;

        public ChatRoomController(IChatRoomServices _services)
        {
            services = _services;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateChatRoom([FromQuery] string receiverId)
        {
            var senderId = GetUserId();
            await services.CreateChatRoom(senderId, receiverId);
            return Ok();
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetChatRooms()
        {
            var senderId = GetUserId();
            var rooms = await services.GetAllChatRoomsByUserIdAsync(senderId);
            return Ok(rooms);
        }

        [HttpPost]
        [Route("create-connection")]
        public async Task<IActionResult> CreateConnection(ChatRoomCreateConnectionModel model)
        {
            var userId = GetUserId();
            await services.CreateConnection(model, userId);
            return Ok();
        }

        [HttpGet]
        [Route("get-connection")]
        public async Task<IActionResult> GetConnections([FromQuery] string chatId)
        {
            var connections = await services.GetConnectionsByChatId(chatId);
            return Ok(connections);
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
