using SuratBook.Services.Models.Comment;

namespace SuratBook.Test.Controller
{
    [TestFixture]
    internal class PostControllerTest
    {
        [Test]
        public async Task DeletePostWithInvalidIdShouldReturnBadRequest()
        {
            //Arrage
            var controller = new PostController(IPostServiceMock.Get());

            //Act
            var result = await controller.DeletePost("invalidId");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            });
        }

        [Test]
        public async Task DeletePostWithValidIdShouldReturnOK()
        {
            //Arrage
            var controller = new PostController(IPostServiceMock.Get());

            //Act
            var result = await controller.DeletePost("validId");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkResult>());
            });
        }

        [Test]
        public async Task GetPostCommentsShouldReturnOkWithCommentsList()
        {
            //Arrage
            var controller = new PostController(IPostServiceMock.Get());

            //Act
            var result = await controller.GetPostComments("postId");
            var data = result as ObjectResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                Assert.That(data.Value, Is.TypeOf<CommentViewModel[]>());
            });
        }
    }
}
