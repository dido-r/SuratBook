namespace SuratBook.Test.Mock
{
    public class IGroupServiceMock
    {
        public static IGroupServices Get()
        {
            var service = new Mock<IGroupServices>();
            service
                .Setup(x => x.GetOwnedGroupsAsync("userId").Result)
                .Returns(new List<GroupViewModel>());

            service
                .Setup(x => x.GetGroupDataAsync("groupId").Result)
                .Returns(new GroupViewModel());

            return service.Object;
        }
    }
}
