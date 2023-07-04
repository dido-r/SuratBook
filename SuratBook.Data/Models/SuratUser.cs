namespace SuratBook.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class SuratUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public Guid LocationId { get; set; }

        public Location Location { get; set; } = null!;

        public Guid EducationId { get; set; }

        public Education Education { get; set; } = null!;

        public bool Online { get; set; } = false;

        public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();

        public HashSet<Photo> Photos { get; set; } = new HashSet<Photo>();

        public HashSet<Group> Groups { get; set; } = new HashSet<Group>();

        public HashSet<SuratUser> Friends { get; set; } = new HashSet<SuratUser>();

        public HashSet<FriendsRequests> FriendsRequests { get; set; } = new HashSet<FriendsRequests>();

        public HashSet<UsersLikedPhotos> LikedPhotos { get; set; } = new HashSet<UsersLikedPhotos>();

        public HashSet<UsersLikedPosts> LikedPosts { get; set; } = new HashSet<UsersLikedPosts>();
    }
}
