namespace SuratBook.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static SuratBook.Data.Constants.Constants;

    public class SuratUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        public string? MainPhoto { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(Location))]
        public Guid? LocationId { get; set; } = null!;

        public Location? Location { get; set; } = null!;

        [ForeignKey(nameof(Education))]
        public Guid? EducationId { get; set; } = null!;

        public Education? Education { get; set; } = null!;

        [Required]
        public bool Online { get; set; } = true;

        public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

        public HashSet<Photo> Photos { get; set; } = new HashSet<Photo>();

        public HashSet<Group> OwnedGroups { get; set; } = new HashSet<Group>();

        public HashSet<FriendRequester> SentFriendsRequests { get; set; } = new HashSet<FriendRequester>();

        public HashSet<FriendRecipient> ReceivedFriendsRequests { get; set; } = new HashSet<FriendRecipient>();

        public HashSet<UsersJoinedGroups> UsersGroups { get; set; } = new HashSet<UsersJoinedGroups>();

        public HashSet<UsersLikedPhotos> LikedPhotos { get; set; } = new HashSet<UsersLikedPhotos>();

        public HashSet<UsersLikedPosts> LikedPosts { get; set; } = new HashSet<UsersLikedPosts>();
    }
}
