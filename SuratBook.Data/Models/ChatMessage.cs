using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(ChatRoom))]
        public Guid ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; } = null!;
    }
}
