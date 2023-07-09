namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FriendRecipient
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(SuratUser))]
        public Guid SuratUserId { get; set; }

        public SuratUser SuratUser { get; set; } = null!;

        public HashSet<FriendsRequests> FriendsRequests { get; set; } = new HashSet<FriendsRequests>();
    }
}
