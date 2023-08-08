using SuratBook.Data;
using SuratBook.Data.Models;
using SuratBook.Services.Models.Comment;
using SuratBook.Services.Models.Post;
using SuratBook.Services.ServiceProviders;

namespace SuratBook.Test.Services
{
    [TestFixture]
    internal class PostServiceTest
    {
        private SuratBookDbContext db;
        private PostServices service;
        private CommentServices commentServ;

        [SetUp]
        public void SetUp()
        {
            var user = new SuratUser
            {
                Id = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                FirstName = "Bai",
                LastName = "Testov",
            };

            db = DatabaseMock.GetDatabase();
            db.Users.Add(user);
            db.SaveChanges();
            service = new PostServices(db);
            commentServ = new CommentServices(db);
        }

        [Test]
        public async Task CommentPostReturnType()
        {
            //Arrange

            //Act
            var result = await commentServ.GetPostCommentsAsync("4344e979-7e1d-45cf-ab20-a6c0fb130aef");

            //Assert
            Assert.That(result, Is.TypeOf<List<CommentViewModel>>());
        }

        [Test]
        public async Task CommentPostReturnValue()
        {
            //Arrange
            var comment = new Comment[] {
                new Comment
                {
                    Content = "Test1",
                    PhotoId = Guid.NewGuid(),
                    PostId = Guid.Parse("4344e979-7e1d-45cf-ab20-a6c0fb130aef"),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                },
                new Comment
                {
                    Content = "Test2",
                    PhotoId = Guid.NewGuid(),
                    PostId = Guid.Parse("4344e979-7e1d-45cf-ab20-a6c0fb130aef"),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                },
                new Comment
                {
                    Content = "Test3",
                    PhotoId = Guid.NewGuid(),
                    PostId = Guid.Parse("7f733a6e-5940-4caa-a4dd-05102d0f646c"),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                }
            };
            db.Comments.AddRange(comment);
            db.SaveChanges();

            //Act
            var result = await commentServ.GetPostCommentsAsync("4344e979-7e1d-45cf-ab20-a6c0fb130aef");

            //Assert
            Assert.That(result.Any(x => x.Content == "Test1"), Is.True);
        }

        [Test]
        public async Task IsPostCreated()
        {
            //Arrange
            var model = new CreatePostFormModel
            {
                Description = "Description",
                OwnerId = "76164e24-b0f1-42cf-96da-c5601aeb7676"
            };

            //Act
            var result = await service.CreatePostAsync(model);
            var postId = db.Posts.First().Id.ToString();

            //Assert
            Assert.That(result.Id.ToString(), Is.EqualTo(postId));
        }

        [Test]
        public async Task IsPostCreatedReturnTypeOfString()
        {
            //Arrange
            var model = new CreatePostFormModel
            {
                Description = "Description",
                OwnerId = "76164e24-b0f1-42cf-96da-c5601aeb7676"
            };

            //Act
            var result = await service.CreatePostAsync(model);

            //Assert
            Assert.That(result, Is.TypeOf<CreatePostResponseModel>());
        }

        [Test]
        public async Task IsPostDeleted()
        {
            //Arrange
            var post = new Post
            {
                Description = "Description"
            };
            db.Posts.Add(post);
            db.SaveChanges();

            //Act
            await service.DeletePostAsync(post.Id.ToString());

            //Assert
            Assert.That(post.IsDeleted, Is.True);
        }

        [Test]
        public async Task IsPostEdited()
        {
            //Arrange
            var post = new Post
            {
                Description = "Description"
            };
            db.Posts.Add(post);
            db.SaveChanges();
            var model = new EditPostFormModel
            {
                Id = post.Id.ToString(),
                Description = "Description was edited"
            };

            //Act
            await service.EditPostAsync(model);

            //Assert
            Assert.That(post.Description, Is.EqualTo("Description was edited"));
        }

        [Test]
        public async Task GetAllPostReturnType()
        {
            //Arrange

            //Act
            var result = await service.GetAllPostsAsync("id", 0, 5);

            //Assert
            Assert.That(result, Is.TypeOf<List<PostViewModel>>());
        }

        [Test]
        public async Task GetAllPostReturnCount()
        {
            //Arrange
            var posts = new Post[]
            {
                new Post
                {
                    Description= "Description1",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                },
                new Post
                {
                    Description= "Description2",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                }
            };
            db.Posts.AddRange(posts);
            db.SaveChanges();

            //Act
            var result = await service.GetAllPostsAsync("id", 0, 5);

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
