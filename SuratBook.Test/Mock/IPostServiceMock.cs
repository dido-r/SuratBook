namespace SuratBook.Test.Mock
{
    public static class IPostServiceMock
    {
        public static IPostServices Get()
        {
            var service = new Mock<IPostServices>();

            service
                .Setup(x => x.DeletePostAsync("invalidId"))
                .Throws<Exception>();

            return service.Object;
        }
    }
}
