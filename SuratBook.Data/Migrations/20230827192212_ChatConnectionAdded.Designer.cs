﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuratBook.Data;

#nullable disable

namespace SuratBook.Data.Migrations
{
    [DbContext(typeof(SuratBookDbContext))]
    [Migration("20230827192212_ChatConnectionAdded")]
    partial class ChatConnectionAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatConnections");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatRoomParticipant", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ChatRoomId");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatRoomParticipants");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PhotoId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PostId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PhotoId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("School")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("University")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UniversityDegreeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UniversityDegreeId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRecipient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SuratUserId");

                    b.ToTable("FriendsRecipients");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRequester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SuratUserId");

                    b.ToTable("FriendsRequesters");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendsRequests", b =>
                {
                    b.Property<Guid>("RequesterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AreFriends")
                        .HasColumnType("bit");

                    b.HasKey("RequesterId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("FriendsRequests");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("GroupInfo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MainPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccessId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SuratBook.Data.Models.GroupAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("GroupAccess");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Private"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Public"
                        });
                });

            modelBuilder.Entity("SuratBook.Data.Models.GroupJoinRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrouptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GrouptId");

                    b.HasIndex("SuratUserId");

                    b.ToTable("GroupsJoinRequests");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Town")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DropboxId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DropboxPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("DropboxPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SuratBook.Data.Models.SuratUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EducationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MainPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Online")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("EducationId");

                    b.HasIndex("LocationId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SuratBook.Data.Models.UniversityDegree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("UniversityDegrees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Master"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bachelor"
                        });
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersJoinedGroups", b =>
                {
                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrouptId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SuratUserId", "GrouptId");

                    b.HasIndex("GrouptId");

                    b.ToTable("UsersJoinedGroups");
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersLikedPhotos", b =>
                {
                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SuratUserId", "PhotoId");

                    b.HasIndex("PhotoId");

                    b.ToTable("UsersLikedPhotos");
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersLikedPosts", b =>
                {
                    b.Property<Guid>("SuratUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SuratUserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("UsersLikedPosts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatConnection", b =>
                {
                    b.HasOne("SuratBook.Data.Models.ChatRoom", "ChatRoom")
                        .WithMany("Connections")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatMessage", b =>
                {
                    b.HasOne("SuratBook.Data.Models.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatRoomParticipant", b =>
                {
                    b.HasOne("SuratBook.Data.Models.ChatRoom", "ChatRoom")
                        .WithMany("ChatRoomParticipants")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Comment", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.Photo", "Photo")
                        .WithMany("Comments")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Photo");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Education", b =>
                {
                    b.HasOne("SuratBook.Data.Models.UniversityDegree", "UniversityDegree")
                        .WithMany()
                        .HasForeignKey("UniversityDegreeId");

                    b.Navigation("UniversityDegree");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRecipient", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany("ReceivedFriendsRequests")
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRequester", b =>
                {
                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany("SentFriendsRequests")
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendsRequests", b =>
                {
                    b.HasOne("SuratBook.Data.Models.FriendRecipient", "Recipient")
                        .WithMany("FriendsRequests")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.FriendRequester", "Requester")
                        .WithMany("FriendsRequests")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Group", b =>
                {
                    b.HasOne("SuratBook.Data.Models.GroupAccess", "Access")
                        .WithMany()
                        .HasForeignKey("AccessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "Owner")
                        .WithMany("OwnedGroups")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Access");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SuratBook.Data.Models.GroupJoinRequest", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GrouptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany()
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Photo", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Group", null)
                        .WithMany("Photos")
                        .HasForeignKey("GroupId");

                    b.HasOne("SuratBook.Data.Models.SuratUser", "Owner")
                        .WithMany("Photos")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Post", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("SuratBook.Data.Models.SuratUser", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SuratBook.Data.Models.SuratUser", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId");

                    b.HasOne("SuratBook.Data.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Education");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersJoinedGroups", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GrouptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany("UsersGroups")
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersLikedPhotos", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Photo", "Photo")
                        .WithMany("UsersLikes")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany("LikedPhotos")
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.UsersLikedPosts", b =>
                {
                    b.HasOne("SuratBook.Data.Models.Post", "Post")
                        .WithMany("UsersLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuratBook.Data.Models.SuratUser", "SuratUser")
                        .WithMany("LikedPosts")
                        .HasForeignKey("SuratUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("SuratUser");
                });

            modelBuilder.Entity("SuratBook.Data.Models.ChatRoom", b =>
                {
                    b.Navigation("ChatRoomParticipants");

                    b.Navigation("Connections");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRecipient", b =>
                {
                    b.Navigation("FriendsRequests");
                });

            modelBuilder.Entity("SuratBook.Data.Models.FriendRequester", b =>
                {
                    b.Navigation("FriendsRequests");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Group", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Photos");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Photo", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UsersLikes");
                });

            modelBuilder.Entity("SuratBook.Data.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UsersLikes");
                });

            modelBuilder.Entity("SuratBook.Data.Models.SuratUser", b =>
                {
                    b.Navigation("LikedPhotos");

                    b.Navigation("LikedPosts");

                    b.Navigation("OwnedGroups");

                    b.Navigation("Photos");

                    b.Navigation("Posts");

                    b.Navigation("ReceivedFriendsRequests");

                    b.Navigation("SentFriendsRequests");

                    b.Navigation("UsersGroups");
                });
#pragma warning restore 612, 618
        }
    }
}