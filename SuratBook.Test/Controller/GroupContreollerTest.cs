namespace SuratBook.Test.Controller
{
    [TestFixture]
    internal class GroupContreollerTest
    {
        [Test]
        public async Task TestGetOwnedGroupsReturnType()
        {
            //Arrange
            var controller = new GroupController(IGroupServiceMock.Get());

            //Act
            var result = await controller.GetOwnedGroups("userId");
            var data = result as OkObjectResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                Assert.That(result, Is.Not.Null);
                Assert.That(data!.Value, Is.TypeOf<List<GroupViewModel>>());
                Assert.That(controller, Is.InstanceOf<ControllerBase>());
            });
        }

        [Test]
        public async Task GetGroupDatReturnType()
        {
            //Arrange
            var controller = new GroupController(IGroupServiceMock.Get());

            //Act
            var result = await controller.GetGroupData("groupId");
            var data = result as OkObjectResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                Assert.That(result, Is.Not.Null);
                Assert.That(data!.Value, Is.TypeOf<GroupViewModel>());
                Assert.That(controller, Is.InstanceOf<ControllerBase>());
            });
        }

        [Test]
        public async Task EditGroupInfoShouldReturnOk()
        {
            //Arrange
            var controller = new GroupController(IGroupServiceMock.Get());
            var model = new GroupInfoEditformModel
            {
                Name = "Name",
                GroupInfo = "Info"
            };

            //Act
            var result = await controller.EditGroupInfo(model);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkResult>());
                Assert.That(result, Is.Not.Null);
                Assert.That(controller, Is.InstanceOf<ControllerBase>());
            });
        }

        [Test]
        public async Task GetMembersShouldReturnOk()
        {
            //Arrange
            var controller = new GroupController(IGroupServiceMock.Get());
            var model = new GroupInfoEditformModel
            {
                Name = "Name",
                GroupInfo = "Info"
            };

            //Act
            var result = await controller.GetGroupMember("groupId");
            var data = result as OkObjectResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                Assert.That(result, Is.Not.Null);
                Assert.That(controller, Is.InstanceOf<ControllerBase>());
                Assert.That(data!.Value, Is.TypeOf<GroupMembersViewModel[]>());
            });
        }

        [Test]
        public async Task GetGroupMediaFilesShouldReturnOk()
        {
            //Arrange
            var controller = new GroupController(IGroupServiceMock.Get());
            var model = new GroupInfoEditformModel
            {
                Name = "Name",
                GroupInfo = "Info"
            };

            //Act
            var result = await controller.GetGroupMediaFiles("groupId");
            var data = result as OkObjectResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                Assert.That(result, Is.Not.Null);
                Assert.That(controller, Is.InstanceOf<ControllerBase>());
                Assert.That(data!.Value, Is.TypeOf<GroupMediaViewModel[]>());
            });
        }
    }
}
