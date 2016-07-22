using Api.Models;
using Api.Validators;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Validators
{
    [TestFixture]
    public class FileUploadValidatorTests
    {
        private Mock<IAutoRenterApiConfiguration> _stubAutoRenterApiConfiguration;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterApiConfiguration = new Mock<IAutoRenterApiConfiguration>();
        }

        [Test]
        public void should_return_true_from_is_valid()
        {
            var mockFileUploadValidator = new Mock<FileUploadValidator>(_stubAutoRenterApiConfiguration.Object) { CallBase = true };
            mockFileUploadValidator.Setup(i => i.IsFileSmall(It.IsAny<int>(), It.IsAny<ResultModel>())).Verifiable();
            mockFileUploadValidator.Setup(i => i.IsFileTypeAccepted(It.IsAny<string>(), It.IsAny<ResultModel>())).Verifiable();
            mockFileUploadValidator.Setup(i => i.GetFileExtension(It.IsAny<string>())).Returns("png").Verifiable();

            var result = mockFileUploadValidator.Object.IsValid(GetTestObjects.ConstructHttpPostedFile(new byte[] {1, 2, 3, 4, 5}, "foo", "image/png"));

            Assert.IsTrue(result.Success);
            mockFileUploadValidator.VerifyAll();
        }

        [Test]
        public void should_return_true_from_is_file_small()
        {
            var mockAutoRenterApiConfiguration = new Mock<IAutoRenterApiConfiguration> { CallBase = true };
            mockAutoRenterApiConfiguration.SetupGet(i => i.MaximumFileSizeInKb).Returns(100).Verifiable();

            var mockFileUploadValidator = new Mock<FileUploadValidator>(mockAutoRenterApiConfiguration.Object) { CallBase = true };

            var resultModel = new ResultModel();
            mockFileUploadValidator.Object.IsFileSmall(102400, resultModel);

            Assert.IsTrue(resultModel.Success);
        }

        [Test]
        public void should_return_false_from_is_file_small()
        {
            var mockAutoRenterApiConfiguration = new Mock<IAutoRenterApiConfiguration> { CallBase = true };
            mockAutoRenterApiConfiguration.SetupGet(i => i.MaximumFileSizeInKb).Returns(100).Verifiable();

            var mockFileUploadValidator = new Mock<FileUploadValidator>(mockAutoRenterApiConfiguration.Object) { CallBase = true };

            var resultModel = new ResultModel();
            mockFileUploadValidator.Object.IsFileSmall(202400, resultModel);

            Assert.IsFalse(resultModel.Success);
            Assert.AreEqual("The file could not be uploaded because the file is too large.  The maximum file size allowed is 100 kb.", resultModel.Messages[0]);
        }

        [Test]
        public void should_return_string_from_GetFileExtension()
        {
            var mockFileUploadValidator = new Mock<FileUploadValidator>(_stubAutoRenterApiConfiguration.Object) { CallBase = true };

            Assert.AreEqual("txt", mockFileUploadValidator.Object.GetFileExtension("foo.txt"));
            Assert.AreEqual("docx", mockFileUploadValidator.Object.GetFileExtension("hello.docx"));
            Assert.AreEqual("txt", mockFileUploadValidator.Object.GetFileExtension("long.file.name.txt"));
        }

        [Test]
        public void should_return_true_from_is_file_type_accepted()
        {
            var mockAutoRenterApiConfiguration = new Mock<IAutoRenterApiConfiguration> { CallBase = true };
            mockAutoRenterApiConfiguration.SetupGet(i => i.AcceptedFileTypes).Returns("jpg, png, gif").Verifiable();

            var mockFileUploadValidator = new Mock<FileUploadValidator>(mockAutoRenterApiConfiguration.Object) { CallBase = true };

            var resultModel = new ResultModel();
            mockFileUploadValidator.Object.IsFileTypeAccepted("png", resultModel);

            Assert.IsTrue(resultModel.Success);
        }

        [Test]
        public void should_return_false_from_is_file_type_accepted()
        {
            var mockAutoRenterApiConfiguration = new Mock<IAutoRenterApiConfiguration> { CallBase = true };
            mockAutoRenterApiConfiguration.SetupGet(i => i.AcceptedFileTypes).Returns("jpg, png, gif").Verifiable();

            var mockFileUploadValidator = new Mock<FileUploadValidator>(mockAutoRenterApiConfiguration.Object) { CallBase = true };

            var resultModel = new ResultModel();
            mockFileUploadValidator.Object.IsFileTypeAccepted("image/foo", resultModel);

            Assert.IsFalse(resultModel.Success);
            Assert.AreEqual("The file could not be uploaded because the file type is not accepted.  Accepted file types are jpg, png, gif.", resultModel.Messages[0]);
        }
    }
}
