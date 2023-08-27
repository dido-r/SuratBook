using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class ChatConnection
    {
        public Guid Id { get; set; }

        public string ConnectionId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(ChatRoom))]
        public Guid ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; } = null!;
    }
}
