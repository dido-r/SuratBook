namespace SuratBook.Data.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }

        public HashSet<ChatRoomParticipant> ChatRoomParticipants { get; set; } = new HashSet<ChatRoomParticipant>();

        public HashSet<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();

        public HashSet<ChatConnection> Connections { get; set; } = new HashSet<ChatConnection>();
    }
}

