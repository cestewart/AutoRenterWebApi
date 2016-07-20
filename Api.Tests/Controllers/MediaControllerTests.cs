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
        public void should_call_GetFileStream_from_index()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new MediaModel
                {
                    ContentType = "image/gif",
                    File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
                }
            };

            var mockGetMedia = new Mock<IGetMedia> { CallBase = true };
            mockGetMedia.Setup(i => i.Execute(101)).Returns(resultModel).Verifiable();

            var mockMediaController = new Mock<MediaController>(_stubErrorHandler.Object, mockGetMedia.Object) { CallBase = true };
            mockMediaController.Setup(i => i.GetFileStream(It.IsAny<ResultModel>())).Returns(new FileStreamResult(((MediaModel)resultModel.Data).File, ((MediaModel)resultModel.Data).ContentType)).Verifiable();

            var result = mockMediaController.Object.Index(101);

            Assert.IsInstanceOf<FileResult>(result);
            mockGetMedia.VerifyAll();
            mockMediaController.VerifyAll();
        }

        [Test]
        public void should_return_default_image_from_index()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new MediaModel
                {
                    ContentType = "image/gif",
                    File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
                }
            };

            var mockGetMedia = new Mock<IGetMedia> { CallBase = true };
            mockGetMedia.Setup(i => i.Execute(It.IsAny<int>())).Returns(new ResultModel { Success = false }).Verifiable();

            var mockMediaController = new Mock<MediaController>(_stubErrorHandler.Object, mockGetMedia.Object) { CallBase = true };
            mockMediaController.Setup(i => i.GetDefaultImageFileStream())
                .Returns(new FileStreamResult(((MediaModel) resultModel.Data).File, ((MediaModel) resultModel.Data).ContentType))
                .Verifiable();

            var result = mockMediaController.Object.Index(101);

            Assert.IsInstanceOf<FileResult>(result);
            mockGetMedia.VerifyAll();
            mockMediaController.VerifyAll();
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

        [Test]
        public void should_return_resized_image_from_GetFileStream()
        {
            var resultModel = new ResultModel
            {
                Data = new MediaModel
                {
                    File = StreamConverter.ConvertByteArrayToStream(new byte[] {1, 2, 3, 4, 5}),
                    ContentType = "image/png"
                }
            };

            var mockMediaController = new Mock<MediaController>(_stubErrorHandler.Object, _stubGetMedia.Object) {CallBase = true};
            mockMediaController.SetupGet(i => i.Resize).Returns(200).Verifiable();
            mockMediaController.Setup(i => i.ResizeImage(It.IsAny<MemoryStream>())).Returns(StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 }));

            var result = mockMediaController.Object.GetFileStream(resultModel);

            Assert.AreEqual(string.Empty, result.FileDownloadName);
            mockMediaController.VerifyAll();
        }

        [Test]
        public void should_return_file_to_be_downloaded_from_GetFileStream()
        {
            var resultModel = new ResultModel
            {
                Data = new MediaModel
                {
                    File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 }),
                    ContentType = "image/pdf",
                    FileName = "testfile.pdf"
                }
            };

            var mockMediaController = new Mock<MediaController>(_stubErrorHandler.Object, _stubGetMedia.Object) { CallBase = true };
            mockMediaController.SetupGet(i => i.Resize).Returns(0).Verifiable();
            mockMediaController.SetupGet(i => i.Download).Returns(true).Verifiable();

            var result = mockMediaController.Object.GetFileStream(resultModel);

            Assert.AreEqual("testfile.pdf", result.FileDownloadName);

            mockMediaController.VerifyAll();
        }

        [Test]
        public void should_return_image_from_GetFileStream()
        {
            var resultModel = new ResultModel
            {
                Data = new MediaModel
                {
                    File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 }),
                    ContentType = "image/png"
                }
            };

            var mockMediaController = new Mock<MediaController>(_stubErrorHandler.Object, _stubGetMedia.Object) { CallBase = true };
            mockMediaController.SetupGet(i => i.Resize).Returns(0).Verifiable();
            mockMediaController.SetupGet(i => i.Download).Returns(false).Verifiable();

            var result = mockMediaController.Object.GetFileStream(resultModel);

            Assert.AreEqual(string.Empty, result.FileDownloadName);
            mockMediaController.VerifyAll();
        }
    }
}
