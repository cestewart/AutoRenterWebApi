using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using Api.Converters;
using Api.Models;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class MediaModelConverterTests
    {
        [Test]
        public void should_return_MediaModel_from_ConvertDatabaseMediaModelToApiMediaModel()
        {
            var media = new Data.Media
            {
                MediaId = 101,
                File = new byte[] { 2, 3, 4, 5, 6 },
                FileName = "foo.txt",
                ContentType = "text/plain"
            };

            var result = MediaModelConverter.ConvertDatabaseMediaModelToApiMediaModel(media);

            Assert.AreEqual(media.MediaId, result.MediaId);
            Assert.AreEqual(media.FileName, result.FileName);
            Assert.AreEqual(media.ContentType, result.ContentType);
            Assert.AreEqual(media.File, StreamConverter.ConvertStreamToByteArray(result.File));
        }

        [Test]
        public void should_return_null_from_ConvertDatabaseMediaModelToApiMediaModel()
        {
            Assert.IsNull(MediaModelConverter.ConvertDatabaseMediaModelToApiMediaModel(null));
        }

        [Test]
        public void should_return_database_media_from_ConvertApiMediaModelToDatabaseMediaModel()
        {
            var media = new MediaModel
            {
                MediaId = 101,
                File = StreamConverter.ConvertByteArrayToStream(new byte[] { 2, 3, 4, 5, 6 }),
                FileName = "foo.txt",
                ContentType = "text/plain"
            };

            var result = MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(media);

            Assert.AreEqual(media.MediaId, result.MediaId);
            Assert.AreEqual(media.FileName, result.FileName);
            Assert.AreEqual(media.ContentType, result.ContentType);
            Assert.AreEqual(media.File, StreamConverter.ConvertByteArrayToStream(result.File));
        }

        [Test]
        public void should_return_null_from_ConvertApiMediaModelToDatabaseMediaModel()
        {
            Assert.IsNull(MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(null));
        }

        [Test]
        public void should_return_updated_database_media_from_ConvertApiMediaModelToDatabaseMediaModel()
        {
            var media = new MediaModel
            {
                MediaId = 101,
                File = StreamConverter.ConvertByteArrayToStream(new byte[] { 2, 3, 4, 5, 6 }),
                FileName = "foo.txt",
                ContentType = "text/plain"
            };

            var databaseMedia = new Data.Media
            {
                MediaId = 101,
                File = new byte[] { 2, 3, 4, 5, 6 },
                FileName = "foo.txt",
                ContentType = "text/plain"
            };

            var result = MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(media, databaseMedia);

            Assert.AreEqual(media.MediaId, result.MediaId);
            Assert.AreEqual(media.FileName, result.FileName);
            Assert.AreEqual(media.ContentType, result.ContentType);
            Assert.AreEqual(media.File, StreamConverter.ConvertByteArrayToStream(result.File));
        }

        [Test]
        public void should_return_null_when_updating_database_media_model_from_ConvertApiMediaModelToDatabaseMediaModel()
        {
            Assert.IsNull(MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(null, new Data.Media()));
        }

        [Test]
        public void should_return_MediaModel_from_ConvertHttpPostedFileToMediaModel()
        {
            var file = GetTestObjects.ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "testfile.png", "image/png");

            var result = MediaModelConverter.ConvertHttpPostedFileToMediaModel(file);

            Assert.AreEqual(0, result.MediaId);
            Assert.AreEqual("image/png", result.ContentType);
            Assert.AreEqual("testfile.png", result.FileName);
            Assert.AreEqual(StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 }), result.File);
        }

        [Test]
        public void should_return_MediaModel_from_ConvertHttpPostedFileToMediaModel_with_media_id()
        {
            var file = GetTestObjects.ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "testfile.png", "image/png");

            var result = MediaModelConverter.ConvertHttpPostedFileToMediaModel(file, 101);

            Assert.AreEqual(101, result.MediaId);
            Assert.AreEqual("image/png", result.ContentType);
            Assert.AreEqual("testfile.png", result.FileName);
            Assert.AreEqual(StreamConverter.ConvertByteArrayToStream(new byte[] { 1, 2, 3, 4, 5 }), result.File);
        }

    }
}
