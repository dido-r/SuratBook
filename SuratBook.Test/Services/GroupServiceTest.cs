namespace SuratBook.Test.Services
{
    using SuratBook.Data;
    using SuratBook.Data.Models;
    using SuratBook.Services.Models.Group;
    using SuratBook.Services.Models.Post;
    using SuratBook.Services.ServiceProviders;
    using SuratBook.Test.Mock;
    using Group = Data.Models.Group;

    [TestFixture]
    internal class GroupServiceTest
    {
        private SuratBookDbContext db;
        private GroupServices service;

        [SetUp]
        public void SetUp()
        {
            var user = new SuratUser
            {
                Id = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                FirstName = "Bai",
                LastName = "Testov",
            };

            var access = new GroupAccess
            {
                Name = "Public",
            };

            db = DatabaseMock.GetDatabase();
            db.GroupAccess.Add(access);
            db.Users.Add(user);
            db.SaveChanges();
            service = new GroupServices(db);
        }

        [Test]
        public void IsGroupCreated()
        {
            //Arrange
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                GroupInfo = "Some info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Groups.Add(group);
            db.SaveChanges();

            //Act
            service.CreateGroupAsync(new GroupCreateFormModel
            {
                Name = "Created",
                GroupInfo = "Some info for create"
            }, "644d3bec-83fe-4052-8ae7-28e780fe3f9a").Wait();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(db.Groups.ToList().Count, Is.EqualTo(2));
                Assert.That(db.Groups.Any(x => x.Name == "Created"));
            });
        }

        [Test]
        public void IsGroupInfoEdited()
        {
            //Arrange
            var group = new Group
            {
                Id = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                Name = "Test",
                GroupInfo = "Some info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Groups.Add(group);
            db.SaveChanges();

            //Act
            service.EditGroupInfoAsync(new GroupInfoEditformModel
            {
                Id = "eb9dab24-9d8e-4cef-a882-4d69f44e4426",
                Name = "TestEdited",
                GroupInfo = "Some info edit"
            }).Wait();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(group.Name, Is.EqualTo("TestEdited"));
                Assert.That(group.GroupInfo, Is.EqualTo("Some info edit"));
            });
        }

        [Test]
        public void ThrowExeptionOnGroupInfoEditWithWrongGroupId()
        {
            //Arrange
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                GroupInfo = "Some info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676")
            };
            db.Groups.Add(group);
            db.SaveChanges();

            //Act
            //Assert
            Assert.Throws<AggregateException>(() =>
            {
                service.EditGroupInfoAsync(new GroupInfoEditformModel
                {
                    Id = "d1ceac27-b9ad-41ea-848e-c9e391eea005",
                    Name = "TestEdited",
                    GroupInfo = "Some info edit"
                }).Wait();
            }, "Group doesn't exist");
        }

        [Test]
        public async Task CheckResultCountOfGetGroupPostMethod()
        {
            //Arrange
            var groupPosts = new Post[]{
                new Post
            {
                Description = "Test1",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            },
            new Post
            {
                Description = "Test2",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            }};
            db.Posts.AddRange(groupPosts);
            db.SaveChanges();
            var service = new GroupServices(db);

            //Act
            var result = await service.GetGroupPostsAsync("eb9dab24-9d8e-4cef-a882-4d69f44e4426");

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CheckResultOfGetGroupPostMethod()
        {
            //Arrange
            var groupPosts = new Post[]{
                new Post
            {
                Description = "Test1",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            },
            new Post
            {
                Description = "Test2",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            }};
            db.Posts.AddRange(groupPosts);
            db.SaveChanges();
            var service = new GroupServices(db);

            //Act
            var result = await service.GetGroupPostsAsync("eb9dab24-9d8e-4cef-a882-4d69f44e4426");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(x => x.Description == "Test1"));
                Assert.That(result.Any(x => x.Description == "Test2"));
            });
        }

        [Test]
        public async Task CheckReturnTypeOfGetGroupPostMethod()
        {
            //Arrange
            var groupPosts = new Post[]{
                new Post
            {
                Description = "Test1",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            },
            new Post
            {
                Description = "Test2",
                GroupId = Guid.Parse("eb9dab24-9d8e-4cef-a882-4d69f44e4426"),
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
            }};
            db.Posts.AddRange(groupPosts);
            db.SaveChanges();
            var service = new GroupServices(db);

            //Act
            var result = await service.GetGroupPostsAsync("eb9dab24-9d8e-4cef-a882-4d69f44e4426");

            //Assert
            Assert.That(result, Is.TypeOf<List<PostViewModel>>());
        }

        [Test]
        public async Task GetOwnedGroupsReturnCount()
        {
            //Arrange
            var groups = new Group[]{
                new Group
            {
                Name = "Group 1",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            },
            new Group
            {
                Name = "Group 2",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            }};
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetOwnedGroupsAsync("76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetOwnedGroupsReturn()
        {
            //Arrange
            var groups = new Group[]{
                new Group
            {
                Name = "Group 1",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            },
            new Group
            {
                Name = "Group 2",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId =1
            }};
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetOwnedGroupsAsync("76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(x => x.Name == "Group 1"));
                Assert.That(result.Any(x => x.Name == "Group 2"));
            });
        }

        [Test]
        public async Task GetOwnedGroupsReturnType()
        {
            //Arrange
            var groups = new Group[]{
                new Group
            {
                Name = "Group 1",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            },
            new Group
            {
                Name = "Group 2",
                GroupInfo = "Info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            }};
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetOwnedGroupsAsync("76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.That(result, Is.TypeOf<List<GroupViewModel>>());
        }

        [Test]
        public async Task GetGroupDataReturn()
        {
            //Arrange
            var group = new Group
            {
                Id = Guid.Parse("f787e9e6-1cde-4001-b766-a2f61097f3aa"),
                Name = "Data",
                GroupInfo = "Some info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            };
            db.Add(group);
            db.SaveChanges();
            //Act
            var result = await service.GetGroupDataAsync(group.Id.ToString());

            ////Assert
            Assert.That(result.Name, Is.EqualTo("Data"));
        }

        [Test]
        public async Task GetGroupDataReturnType()
        {
            //Arrange
            var group = new Group
            {
                Id = Guid.Parse("f787e9e6-1cde-4001-b766-a2f61097f3aa"),
                Name = "Data",
                GroupInfo = "Some info",
                OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                AccessId = 1
            };
            db.Groups.Add(group);
            db.SaveChanges();
            //Act
            var result = await service.GetGroupDataAsync(group.Id.ToString());

            ////Assert
            Assert.That(result, Is.TypeOf<GroupViewModel>());
        }

        [Test]
        public async Task GetAllGroupsReturnCount()
        {
            //Arrange
            var groups = new Group[] {
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data1",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                },
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data2",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                }
            };
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetAllGroupsAsync();

            ////Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllGroupsReturnType()
        {
            //Arrange
            var groups = new Group[] {
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data1",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                },
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data2",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                }
            };
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetAllGroupsAsync();

            ////Assert
            Assert.That(result, Is.TypeOf<List<GroupViewModel>>());
        }

        [Test]
        public async Task GetAllGroupsReturn()
        {
            //Arrange
            var groups = new Group[] {
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data1",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                },
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Data2",
                    GroupInfo = "Some info",
                    OwnerId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                    AccessId = 1
                }
            };
            db.Groups.AddRange(groups);
            db.SaveChanges();

            //Act
            var result = await service.GetAllGroupsAsync();

            ////Assert
            Assert.Multiple(() =>
            {
                Assert.That(db.Groups.Any(x => x.Name == "Data1"));
                Assert.That(db.Groups.Any(x => x.Name == "Data2"));
            });
        }

        [Test]
        public async Task IsMemberWithCorrectUserIdandGroupId()
        {
            //Arrange
            var join = new UsersJoinedGroups
            {
                SuratUserId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                GrouptId = Guid.Parse("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d"),
            };
            db.UsersJoinedGroups.AddRange(join);
            db.SaveChanges();

            //Act
            var result = await service.IsMember("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            ////Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsMemberWithInvalidUserIdorGroupId()
        {
            //Arrange
            var join = new UsersJoinedGroups
            {
                SuratUserId = Guid.Parse("76164e24-b0f1-42cf-96da-c5601aeb7676"),
                GrouptId = Guid.Parse("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d"),
            };
            db.UsersJoinedGroups.AddRange(join);
            db.SaveChanges();

            //Act
            var invalidUser = await service.IsMember("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "0e141566-0d1c-4ff0-9277-b4784cddad61");
            var invalidGroup = await service.IsMember("f7baf4d3-4319-4285-8edb-313c02f729ee", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(invalidUser, Is.False);
                Assert.That(invalidGroup, Is.False);
            });
        }

        [Test]
        public async Task JoinGroup()
        {
            //Arrange
            //Act
            await service.JoinGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");
            var userId = db.UsersJoinedGroups.First().SuratUserId.ToString();
            var groupId = db.UsersJoinedGroups.First().GrouptId.ToString();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(userId, Is.EqualTo("76164e24-b0f1-42cf-96da-c5601aeb7676"));
                Assert.That(groupId, Is.EqualTo("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d"));
            });
        }

        [Test]
        public async Task JoinPrivateGroup()
        {
            //Arrange
            //Act
            await service.JoinPrivateGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");
            var userId = db.GroupsJoinRequests.First().SuratUserId.ToString();
            var groupId = db.GroupsJoinRequests.First().GrouptId.ToString();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(userId, Is.EqualTo("76164e24-b0f1-42cf-96da-c5601aeb7676"));
                Assert.That(groupId, Is.EqualTo("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d"));
            });
        }

        [Test]
        public async Task LeaveGroup()
        {
            //Arrange
            await service.JoinGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");
            var before = db.UsersJoinedGroups.ToArray().Length;

            //Act
            await service.LeaveGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");
            var after = db.UsersJoinedGroups.ToArray().Length;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(before, Is.EqualTo(1));
                Assert.That(after, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task IsPendingJoinRequestsAsyncTrue()
        {
            //Arrange
            await service.JoinPrivateGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Act
            var result = await service.IsPendingJoinRequestsAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsPendingJoinRequestsAsyncFalse()
        {
            //Arrange
            await service.JoinPrivateGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Act
            var result = await service.IsPendingJoinRequestsAsync("9f09v9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76185e24-b0f1-42cf-96da-c5601aeb7676");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetPendingJoinRequestsType()
        {
            //Arrange
            await service.JoinPrivateGroupAsync("9f01b9ee-d1f6-4e75-8ebe-1b4e4d8ac27d", "76164e24-b0f1-42cf-96da-c5601aeb7676");

            //Act
            var result = await service.GetPendingJoinRequestsAsync("9f09v9ee-d1f6-4e75-8ebe-1b4e4d8ac27d");

            //Assert
            Assert.That(result, Is.TypeOf<List<RequestUserViewModel>>());
        }
    }
}
