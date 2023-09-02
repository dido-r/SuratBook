namespace SuratBook.Data.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }

        public bool Notifications { get; set; } = false;

        public HashSet<ChatRoomParticipant> ChatRoomParticipants { get; set; } = new HashSet<ChatRoomParticipant>();

        public HashSet<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();
    }
}

