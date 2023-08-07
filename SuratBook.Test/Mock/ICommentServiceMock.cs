namespace SuratBook.Test.Mock
{
    public class ICommentServiceMock
    {
        public static ICommentServices Get()
        {
            var service = new Mock<ICommentServices>();

            return service.Object;
        }
    }
}
