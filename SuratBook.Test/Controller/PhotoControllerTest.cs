namespace SuratBook.Test.Controller
{
    [TestFixture]
    public class PhotoControllerTest
    {
        [Test]
        public async Task DeletePhotoWithInvalidIdShouldreTurnErrorModel()
        {
            //Arrage
            var controller = new PhotoController(IPhotoServiceMock.Get());

            //Act
            var result = await controller.DeletePhotoAsync("invalidId");
            var data = result as ObjectResult;
            var errormodel = (ValidationError)data!.Value!;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<ObjectResult>());
                Assert.That(errormodel.Message, Is.EqualTo("Could not delete the photo"));
            });
        }

        [Test]
        public async Task DeletePhotoWithValidIdShouldreTurnOk()
        {
            //Arrage
            var controller = new PhotoController(IPhotoServiceMock.Get());

            //Act
            var result = await controller.DeletePhotoAsync("validId");

            //Assert
            Assert.That(result, Is.TypeOf<OkResult>());
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task CreatePhotoShouldReturnString()
        {
            //Arrage
            var controller = new PhotoController(IPhotoServiceMock.Get());

            //Act
            var result = await controller.CreatePhoto(new CreatePhotoModel() { });

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task FindByPathShouldReturnOkWithBool()
        {
            //Arrage
            var controller = new PhotoController(IPhotoServiceMock.Get());

            //Act
            var result = await controller.FindByPathAsync("photoId");
            var data = result as ObjectResult;

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
            Assert.That(data!.Value, Is.TypeOf<bool>());
        }
    }
}
