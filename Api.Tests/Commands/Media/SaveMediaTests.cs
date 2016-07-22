using System.Collections.Generic;
using Api.Commands.Media;
using Api.Converters;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Media
{
    [TestFixture]
    public class SaveMediaTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

        private static FakeDbSet<Data.Media> GetMockedMediaData()
        {
            var medias = new List<Data.Media>
            {
                new Data.Media
                {
                    MediaId = 101,
                    ContentType = "image/jpeg",
                    File = new byte[] {1,2,3,4,5}
                },
                new Data.Media
                {
                    MediaId = 102,
                    ContentType = "image/gif",
                    File = new byte[] {5,5,5,5,5}
                }
            };
            var mediaDbSet = new FakeDbSet<Data.Media>();
            mediaDbSet.SetData(medias);
            return mediaDbSet;
        }

        [Test]
        public void Execute_should_create_new_media_record_by_calling_CreateMedia()
        {
            var mockSaveMedia = new Mock<SaveMedia>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveMedia.Setup(i => i.CreateMedia(It.IsAny<MediaModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveMedia.Object.Execute(new MediaModel());

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveMedia.Verify(i => i.CreateMedia(It.IsAny<MediaModel>()), Times.Once);
            mockSaveMedia.Verify(i => i.UpdateMedia(It.IsAny<MediaModel>()), Times.Never);
        }

        [Test]
        public void Execute_should_update_media_record_by_calling_UpdateMedia()
        {
            var mockSaveMedia = new Mock<SaveMedia>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveMedia.Setup(i => i.UpdateMedia(It.IsAny<MediaModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveMedia.Object.Execute(new MediaModel { MediaId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveMedia.Verify(i => i.CreateMedia(It.IsAny<MediaModel>()), Times.Never);
            mockSaveMedia.Verify(i => i.UpdateMedia(It.IsAny<MediaModel>()), Times.Once);
        }

        [Test]
        public void CreateMedia_should_create_media_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Medias).Returns(GetMockedMediaData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveMedia = new Mock<SaveMedia>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var mediaModel = new MediaModel
            {
                File = StreamConverter.ConvertByteArrayToStream(new byte[] {1,2,3,4,5})
            };
            var result = mockSaveMedia.Object.CreateMedia(mediaModel);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void UpdateMedia_should_update_media_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Medias).Returns(GetMockedMediaData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveMedia = new Mock<SaveMedia>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var mediaModel = new MediaModel
            {
                MediaId = 101,
                File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
            };
            var result = mockSaveMedia.Object.UpdateMedia(mediaModel);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void UpdateMedia_should_handle_missing_media_when_updating_media_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Medias).Returns(GetMockedMediaData().Object).Verifiable();

            var mockSaveMedia = new Mock<SaveMedia>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var mediaModel = new MediaModel
            {
                MediaId = 555,
                File = StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 })
            };
            var result = mockSaveMedia.Object.UpdateMedia(mediaModel);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(1, result.Messages.Count);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

    }
}
