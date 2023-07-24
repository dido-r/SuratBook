using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Data.Models
{
    public class FriendsRequests
    {
        [Required]
        [ForeignKey(nameof(Requester))]
        public Guid RequesterId { get; set; }

        public FriendRequester Requester { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Recipient))]
        public Guid RecipientId { get; set; }

        public FriendRecipient Recipient { get; set; } = null!;

        [Required]
        public bool AreFriends { get; set; } = false;
    }
}
