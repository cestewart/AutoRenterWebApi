using System.Collections.Generic;
using Api.Commands.Media;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Media
{
    [TestFixture]
    public class GetMediaTests
    {
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
        public void should_return_media_from_Execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Medias).Returns(GetMockedMediaData().Object).Verifiable();

            var mockGetMedia = new Mock<GetMedia>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetMedia.Object.Execute(101);

            Assert.AreEqual("image/jpeg", ((MediaModel)result.Data).ContentType);
            Assert.AreEqual(101, ((MediaModel)result.Data).MediaId);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void should_handle_missing_media_from_Execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Medias).Returns(GetMockedMediaData().Object).Verifiable();

            var mockGetMedia = new Mock<GetMedia>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetMedia.Object.Execute(303);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("The media could not be found.", result.Messages[0]);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

    }
}
