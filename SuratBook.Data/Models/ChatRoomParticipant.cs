namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ChatRoomParticipant
    {
        [ForeignKey(nameof(SuratUser))]
        public Guid UserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        [ForeignKey(nameof(ChatRoom))]
        public Guid ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; } = null!;
    }
}
