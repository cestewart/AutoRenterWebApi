using System;
using System.Web.Mvc;
using Api.Commands.Branding;
using Api.Controllers;
using Api.Converters;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class ImageControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetBranding> _stubGetBranding;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetBranding = new Mock<IGetBranding>();
        }

        [Test]
        public void should_return_image_from_index()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new BrandingModel
                {
                    ContentType = "image/gif",
                    Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
                }
            };

            var mockGetBranding = new Mock<IGetBranding> {CallBase = true};
            mockGetBranding.Setup(i => i.Execute("logo")).Returns(resultModel).Verifiable();

            var mockImageController = new Mock<ImageController>(_stubErrorHandler.Object, mockGetBranding.Object) {CallBase = true};

            var result = mockImageController.Object.Index("logo");

            Assert.IsInstanceOf<FileResult>(result);
            mockGetBranding.VerifyAll();
        }

        [Test]
        public void should_return_default_image_from_index()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new BrandingModel
                {
                    ContentType = "image/gif",
                    Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
                }
            };

            var mockGetBranding = new Mock<IGetBranding> { CallBase = true };
            mockGetBranding.Setup(i => i.Execute("missing")).Returns(new ResultModel {Success = false}).Verifiable();
            mockGetBranding.Setup(i => i.Execute("default")).Returns(resultModel).Verifiable();

            var mockImageController = new Mock<ImageController>(_stubErrorHandler.Object, mockGetBranding.Object) { CallBase = true };

            var result = mockImageController.Object.Index("missing");

            Assert.IsInstanceOf<FileResult>(result);
            mockGetBranding.VerifyAll();
        }

        [Test]
        public void should_handle_exception_from_index()
        {
            var mockErrorHandler = new Mock<IErrorHandler> {CallBase = true};
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockGetBranding = new Mock<IGetBranding> { CallBase = true };
            mockGetBranding.Setup(i => i.Execute("logo")).Throws(new Exception("An error occured")).Verifiable();

            var mockImageController = new Mock<ImageController>(mockErrorHandler.Object, mockGetBranding.Object) { CallBase = true };

            var result = mockImageController.Object.Index("logo");

            Assert.IsInstanceOf<FileResult>(result);
            mockGetBranding.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

    }
}
