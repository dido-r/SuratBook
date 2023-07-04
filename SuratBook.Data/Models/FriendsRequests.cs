namespace SuratBook.Data.Models
{
    public class FriendsRequests
    {
        public Guid RequesterId { get; set; }

        public SuratUser Requester { get; set; } = null!;

        public Guid RecipientId { get; set; }

        public SuratUser Recipient { get; set; } = null!;
    }
}
