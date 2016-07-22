using System;
using System.IO;
using System.Web.Mvc;
using Api.Commands.Media;
using Api.Controllers;
using Api.Converters;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class MediaControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetMedia> _stubGetMedia;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetMedia = new Mock<IGetMedia>();
        }

        [Test]
        public void should_handle_exception_from_index()
        {
            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockGetMedia = new Mock<IGetMedia> { CallBase = true };
            mockGetMedia.Setup(i => i.Execute(101)).Throws(new Exception()).Verifiable();

            var mockMediaController = new Mock<MediaController>(mockErrorHandler.Object, mockGetMedia.Object) { CallBase = true };

            var result = mockMediaController.Object.Index(101);

            Assert.IsInstanceOf<FileResult>(result);
            mockGetMedia.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

    }
}
