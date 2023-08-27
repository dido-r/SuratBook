namespace SuratBook.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using SuratBook.Data.Models;

    public class SuratBookDbContext : IdentityDbContext<SuratUser, IdentityRole<Guid>, Guid>
    {
        public SuratBookDbContext(DbContextOptions<SuratBookDbContext> options)
            : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<ChatRoom> ChatRooms { get; set; } = null!;
        public DbSet<ChatConnection> ChatConnections { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public DbSet<ChatRoomParticipant> ChatRoomParticipants { get; set; } = null!;
        public DbSet<GroupAccess> GroupAccess { get; set; } = null!;
        public DbSet<UniversityDegree> UniversityDegrees { get; set; } = null!;
        public DbSet<FriendsRequests> FriendsRequests { get; set; } = null!;
        public DbSet<FriendRequester> FriendsRequesters { get; set; } = null!;
        public DbSet<FriendRecipient> FriendsRecipients { get; set; } = null!;
        public DbSet<UsersJoinedGroups> UsersJoinedGroups { get; set; } = null!;
        public DbSet<UsersLikedPhotos> UsersLikedPhotos { get; set; } = null!;
        public DbSet<UsersLikedPosts> UsersLikedPosts { get; set; } = null!;
        public DbSet<GroupJoinRequest> GroupsJoinRequests { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>()
                .Property(p => p.GroupId)
                .IsRequired(required: false);

            builder.Entity<Education>()
                .Property(p => p.UniversityDegreeId)
                .IsRequired(required: false);

            builder
                .Entity<FriendsRequests>()
                .HasKey(x => new
                {
                    x.RequesterId,
                    x.RecipientId
                });

            builder
                .Entity<ChatRoomParticipant>()
                .HasKey(x => new
                {
                    x.UserId,
                    x.ChatRoomId
                });

            builder
                .Entity<UsersLikedPhotos>()
                .HasKey(x => new
                {
                    x.SuratUserId,
                    x.PhotoId
                });

            builder
                .Entity<UsersLikedPosts>()
                .HasKey(x => new
                {
                    x.SuratUserId,
                    x.PostId
                });

            builder
                .Entity<UsersJoinedGroups>()
                .HasKey(x => new
                {
                    x.SuratUserId,
                    x.GrouptId
                });

            builder
                .Entity<Comment>()
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Entity<Post>()
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Entity<Group>()
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Entity<Photo>()
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Entity<ChatMessage>()
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Entity<GroupAccess>()
                .HasData(GenerateAccess());

            builder
                .Entity<UniversityDegree>()
                .HasData(GenerateDegree());

            base.OnModelCreating(builder);
        }
        private GroupAccess[] GenerateAccess()
        {
            ICollection<GroupAccess> accessList = new HashSet<GroupAccess>();

            GroupAccess access;

            access = new GroupAccess()
            {
                Id = 1,
                Name = "Private"
            };
            accessList.Add(access);

            access = new GroupAccess()
            {
                Id = 2,
                Name = "Public"
            };
            accessList.Add(access);

            return accessList.ToArray();
        }

        private UniversityDegree[] GenerateDegree()
        {
            ICollection<UniversityDegree> degreeList = new HashSet<UniversityDegree>();

            UniversityDegree degree;

            degree = new UniversityDegree()
            {
                Id = 1,
                Name = "Doctor"
            };
            degreeList.Add(degree);

            degree = new UniversityDegree()
            {
                Id = 2,
                Name = "Master"
            };
            degreeList.Add(degree);

            degree = new UniversityDegree()
            {
                Id = 3,
                Name = "Bachelor"
            };
            degreeList.Add(degree);

            return degreeList.ToArray();
        }
    }
}