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

        [HttpPost]
        [Route("create-message")]
        public async Task<IActionResult> CreateMEssage(ChatMessageCreateModel message)
        {
            await services.CreateMessageAsync(message);
            return Ok();
        }

        [HttpGet]
        [Route("get-messages")]
        public async Task<IActionResult> GetChatMessages([FromQuery] string chatId, int offset, int messageLimit)
        {
            var messages = await services.GetChatMessages(chatId, offset, messageLimit);
            return Ok(messages);
        }

        [HttpPost]
        [Route("set-notification")]
        public async Task<IActionResult> SetNotification([FromQuery] string chatId, string param)
        {
           await services.SetNotificationAsync(chatId, param);
           return Ok();
        }

        private string GetUserId()
        {
            return Request.Cookies["surat_auth"]!;
        }
    }
}
