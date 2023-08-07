namespace SuratBook.Test.Services
{
    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Models.Comment;
    using SuratBook.Services.Models.Photo;
    using SuratBook.Services.ServiceProviders;
    using SuratBook.Test.Mock;

    [TestFixture]
    internal class PhotoServiceTest
    {
        private SuratBookDbContext db;
        private PhotoServices service;
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
            service = new PhotoServices(db);
            commentServ = new CommentServices(db);
        }

        [Test]
        public async Task IsPhotoCreated()
        {
            //Arrange
            var photo = new CreatePhotoModel
            {
                DropboxId = "id",
                DropboxPath = "path",
                OwnerId = "76164e24-b0f1-42cf-96da-c5601aeb7676"
            };

            //Act
            var result = await service.CreatePhotoAsync(photo);

            //Assert
            Assert.That(db.Photos.Any(x => x.DropboxPath == "path"), Is.True);
        }

        [Test]
        public async Task IsPhotoCreatedReturnValue()
        {
            //Arrange
            var photoModel = new CreatePhotoModel
            {
                DropboxId = "id",
                DropboxPath = "path",
                OwnerId = "76164e24-b0f1-42cf-96da-c5601aeb7676"
            };

            //Act
            var result = await service.CreatePhotoAsync(photoModel);
            var photo = db.Photos.First();

            //Assert
            Assert.That(result, Is.EqualTo(photo.Id.ToString()));
        }

        [Test]
        public async Task IsPhotoDeleted()
        {
            //Arrange
            var photo = new Photo
            {
                DropboxId = "id",
                DropboxPath = "path",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Photos.Add(photo);
            db.SaveChanges();

            //Act
            await service.DeletePhotoAsync(photo.Id.ToString());
            var count = db.Photos.Count();

            //Assert
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task IsTrueWhenFindPhotoByCorrectPath()
        {
            //Arrange
            var photo = new Photo
            {
                DropboxId = "id",
                DropboxPath = "path",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Photos.Add(photo);
            db.SaveChanges();

            //Act
            var result = await service.FindByPathAsync(photo.DropboxPath);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsFalseWhenFindPhotoByInvalidPath()
        {
            //Arrange
            var photo = new Photo
            {
                DropboxId = "id",
                DropboxPath = "path",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Photos.Add(photo);
            db.SaveChanges();

            //Act
            var result = await service.FindByPathAsync("other path name");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetPhotoCommentReturnType()
        {
            //Arrange

            //Act
            var result = await commentServ.GetPhotoCommentsAsync("7f733a6e-5940-4caa-a4dd-05102d0f646c");

            //Assert
            Assert.That(result, Is.TypeOf<List<CommentViewModel>>());
        }

        [Test]
        public async Task PhotoCommentReturnValue()
        {
            //Arrange
            var comment = new Comment[] {
                new Comment
                {
                    Content = "Test1",
                    PhotoId = Guid.Parse("be2eb31c-1d4a-4c88-b9cb-c1fdf3d983b5"),
                    PostId = Guid.NewGuid(),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                },
                new Comment
                {
                    Content = "Test2",
                    PhotoId = Guid.Parse("be2eb31c-1d4a-4c88-b9cb-c1fdf3d983b5"),
                    PostId = Guid.NewGuid(),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                },
                new Comment
                {
                    Content = "Test3",
                    PhotoId = Guid.Parse("7f733a6e-5940-4caa-a4dd-05102d0f646c"),
                    PostId = Guid.NewGuid(),
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
                }
            };
            db.Comments.AddRange(comment);
            db.SaveChanges();

            //Act
            var result = await commentServ.GetPhotoCommentsAsync("be2eb31c-1d4a-4c88-b9cb-c1fdf3d983b5");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(x => x.Content == "Test1"), Is.True);
                Assert.That(result.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetPhotoReturnType()
        {
            //Arrange

            //Act
            var result = await service.GetPhotosAsync("be2eb31c-1d4a-4c88-b9cb-c1fdf3d983b5", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.That(result, Is.TypeOf<List<PhotoViewModel>>());
        }

        [Test]
        public async Task GetPhotoReturnValue()
        {
            //Arrange
            var photo1 = new Photo
            {
                DropboxId = "id1",
                DropboxPath = "path1",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            var photo2 = new Photo
            {
                DropboxId = "id2",
                DropboxPath = "path2",
                OwnerId = Guid.Parse("be2eb31c-1d4a-4c88-b9cb-c1fdf3d983b5")
            };
            db.Photos.AddRange(new Photo[] { photo1, photo2 });
            db.SaveChanges();

            //Act
            var result = await service.GetPhotosAsync("76164e24-b0f1-42cf-96da-c5601aeb7676", "5aa066a5-352e-4f3c-92ca-f2bb8c919f0c");

            //Assert
            Assert.That(result.First().DropboxPath, Is.EqualTo("path1"));
        }
    }
}