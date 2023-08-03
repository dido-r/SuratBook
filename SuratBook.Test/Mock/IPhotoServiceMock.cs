namespace SuratBook.Test.Mock
{
    public class IPhotoServiceMock
    {
        public static IPhotoServices Get()
        {
            var service = new Mock<IPhotoServices>();

            service
                .Setup(x => x.DeletePhotoAsync("invalidId"))
                .Throws<Exception>();

            return service.Object;
        }
    }
}
