using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Api.Commands.Media;
using Api.Controllers;
using Api.Converters;
using Api.Models;
using Api.Validators;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class MediaUploadControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<ISaveMedia> _stubSaveMedia;
        private Mock<IFileUploadValidator> _stubFileUploadValidator;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubSaveMedia = new Mock<ISaveMedia>();
            _stubFileUploadValidator = new Mock<IFileUploadValidator>();
        }

        [Test]
        public void post_should_call_put()
        {
            var mockMediaUploadController = new Mock<MediaUploadController>(_stubErrorHandler.Object, _stubSaveMedia.Object, _stubFileUploadValidator.Object) { CallBase = true };
            mockMediaUploadController.Setup(i => i.Put(0)).Returns(It.IsAny<IHttpActionResult>()).Verifiable();

            mockMediaUploadController.Object.Post();

            mockMediaUploadController.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_Put_when_file_is_invalid()
        {
            var mockFileUploadValidator = new Mock<IFileUploadValidator> { CallBase = true };
            mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Returns(new ResultModel { Success = false, Message = "File is invalid."}).Verifiable();

            var mockSaveMedia = new Mock<ISaveMedia> { CallBase = true };
            mockSaveMedia.Setup(i => i.Execute(It.IsAny<MediaModel>())).Returns(new ResultModel { Success = true }).Verifiable();

            var mockMediaUploadController = new Mock<MediaUploadController>(_stubErrorHandler.Object, mockSaveMedia.Object, mockFileUploadValidator.Object) { CallBase = true };
            mockMediaUploadController.Setup(i => i.GetHttpPostedFile()).Returns(GetTestObjects.ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "testfile.png", "image/png")).Verifiable();

            var result = mockMediaUploadController.Object.Put(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("File is invalid.", ((BadRequestErrorMessageResult)result).Message);

            mockFileUploadValidator.VerifyAll();
            mockSaveMedia.Verify(i => i.Execute(It.IsAny<MediaModel>()), Times.Never);
            mockMediaUploadController.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_Put_when_exception_occurs()
        {
            var mockFileUploadValidator = new Mock<IFileUploadValidator> { CallBase = true };
            mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Returns(new ResultModel { Success = true }).Verifiable();

            var mockSaveMedia = new Mock<ISaveMedia> { CallBase = true };
            mockSaveMedia.Setup(i => i.Execute(It.IsAny<MediaModel>())).Throws(new Exception("An error has occured.")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> {CallBase = true};
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockMediaUploadController = new Mock<MediaUploadController>(mockErrorHandler.Object, mockSaveMedia.Object, mockFileUploadValidator.Object) { CallBase = true };
            mockMediaUploadController.Setup(i => i.GetHttpPostedFile()).Returns(GetTestObjects.ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "testfile.png", "image/png")).Verifiable();

            var result = mockMediaUploadController.Object.Put(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error has occured.", ((BadRequestErrorMessageResult)result).Message);

            mockFileUploadValidator.VerifyAll();
            mockSaveMedia.VerifyAll();
            mockErrorHandler.VerifyAll();
            mockMediaUploadController.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_Put()
        {
            var mockFileUploadValidator = new Mock<IFileUploadValidator> { CallBase = true };
            mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Returns(new ResultModel {Success = true}).Verifiable();

            var mockSaveMedia = new Mock<ISaveMedia> {CallBase = true};
            mockSaveMedia.Setup(i => i.Execute(It.IsAny<MediaModel>())).Returns(new ResultModel {Success = true}).Verifiable();

            var mockMediaUploadController = new Mock<MediaUploadController>(_stubErrorHandler.Object, mockSaveMedia.Object, mockFileUploadValidator.Object) { CallBase = true };
            mockMediaUploadController.Setup(i => i.GetHttpPostedFile()).Returns(GetTestObjects.ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "testfile.png", "image/png")).Verifiable();

            var result = mockMediaUploadController.Object.Put(101);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            Assert.IsTrue(((OkNegotiatedContentResult<ResultModel>)result).Content.Success);

            mockFileUploadValidator.VerifyAll();
            mockSaveMedia.VerifyAll();
            mockMediaUploadController.VerifyAll();
        }
    }
}
