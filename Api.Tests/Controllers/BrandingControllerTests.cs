using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http.Results;
using Api.Commands.Branding;
using Api.Controllers;
using Api.Converters;
using Api.Models;
using Api.Validators;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class BrandingControllerTests
    {
        [TestFixture]
        public class LocationControllerTests
        {
            private Mock<IErrorHandler> _stubErrorHandler;
            private Mock<IFileUploadValidator> _stubFileUploadValidator;
            private Mock<IGetBranding> _stubGetBranding;
            private Mock<ISaveBranding> _stubSaveBranding;

            [SetUp]
            public void SetUp()
            {
                _stubErrorHandler = new Mock<IErrorHandler>();
                _stubFileUploadValidator = new Mock<IFileUploadValidator>();
                _stubGetBranding = new Mock<IGetBranding>();
                _stubSaveBranding = new Mock<ISaveBranding>();
            }

            public HttpPostedFile ConstructHttpPostedFile(byte[] data, string filename, string contentType)
            {
                var systemWebAssembly = typeof(HttpPostedFileBase).Assembly;
                var typeHttpRawUploadedContent = systemWebAssembly.GetType("System.Web.HttpRawUploadedContent");
                var typeHttpInputStream = systemWebAssembly.GetType("System.Web.HttpInputStream");

                Type[] uploadedParams = { typeof(int), typeof(int) };
                Type[] streamParams = { typeHttpRawUploadedContent, typeof(int), typeof(int) };
                Type[] parameters = { typeof(string), typeof(string), typeHttpInputStream };

                var uploadedContent = typeHttpRawUploadedContent
                  .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, uploadedParams, null)
                  .Invoke(new object[] { data.Length, data.Length });

                typeHttpRawUploadedContent
                  .GetMethod("AddBytes", BindingFlags.NonPublic | BindingFlags.Instance)
                  .Invoke(uploadedContent, new object[] { data, 0, data.Length });

                typeHttpRawUploadedContent
                  .GetMethod("DoneAddingBytes", BindingFlags.NonPublic | BindingFlags.Instance)
                  .Invoke(uploadedContent, null);

                object stream = (Stream)typeHttpInputStream
                  .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, streamParams, null)
                  .Invoke(new[] { uploadedContent, 0, data.Length });

                var postedFile = (HttpPostedFile)typeof(HttpPostedFile)
                  .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, null)
                  .Invoke(new[] { filename, contentType, stream });

                return postedFile;
            }

            [Test]
            public void should_return_ok_from_get()
            {
                var mockGetBranding = new Mock<IGetBranding> { CallBase = true };
                mockGetBranding.Setup(i => i.Execute(It.IsAny<string>())).Returns(new ResultModel { Success = true }).Verifiable();

                var mockBrandingController = new Mock<BrandingController>(_stubErrorHandler.Object, _stubFileUploadValidator.Object, mockGetBranding.Object, _stubSaveBranding.Object) {CallBase = true};

                var result = mockBrandingController.Object.Get("logo");

                Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
                var contentResult = result as OkNegotiatedContentResult<ResultModel>;
                Assert.IsTrue(contentResult.Content.Success);
                mockGetBranding.VerifyAll();
            }

            [Test]
            public void should_return_bad_request_from_get()
            {
                var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
                mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

                var mockGetBranding = new Mock<IGetBranding> { CallBase = true };
                mockGetBranding.Setup(i => i.Execute(It.IsAny<string>())).Throws(new Exception("An error occured")).Verifiable();

                var mockBrandingController = new Mock<BrandingController>(mockErrorHandler.Object, _stubFileUploadValidator.Object, mockGetBranding.Object, _stubSaveBranding.Object) { CallBase = true };

                var result = mockBrandingController.Object.Get("logo");

                Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
                mockGetBranding.VerifyAll();
                mockErrorHandler.VerifyAll();
            }

            [Test]
            public void should_return_ok_from_post()
            {
                var mockFileUploadValidator = new Mock<IFileUploadValidator> {CallBase = true};
                mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Returns(new ResultModel { Success = true }).Verifiable();

                var mockSaveBranding = new Mock<ISaveBranding> {CallBase = true};
                mockSaveBranding.Setup(i => i.Execute(It.IsAny<BrandingModel>())).Returns(new ResultModel { Success = true }).Verifiable();

                var mockBrandingController = new Mock<BrandingController>(_stubErrorHandler.Object, mockFileUploadValidator.Object, _stubGetBranding.Object, mockSaveBranding.Object) { CallBase = true };
                mockBrandingController.Setup(i => i.GetHttpPostedFile()).Returns(ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "foo", "image/png")).Verifiable();

                var result = mockBrandingController.Object.Post("logo");

                Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
                var contentResult = result as OkNegotiatedContentResult<ResultModel>;
                Assert.IsTrue(contentResult.Content.Success);

                mockBrandingController.VerifyAll();
                mockFileUploadValidator.VerifyAll();
                mockSaveBranding.VerifyAll();
            }

            [Test]
            public void should_return_ok_from_post_and_handle_invalid_file()
            {
                var mockFileUploadValidator = new Mock<IFileUploadValidator> { CallBase = true };
                mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Returns(new ResultModel { Success = false }).Verifiable();

                var mockBrandingController = new Mock<BrandingController>(_stubErrorHandler.Object, mockFileUploadValidator.Object, _stubGetBranding.Object, _stubSaveBranding.Object) { CallBase = true };
                mockBrandingController.Setup(i => i.GetHttpPostedFile()).Returns(ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "foo", "image/png")).Verifiable();

                var result = mockBrandingController.Object.Post("logo");

                Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
                var contentResult = result as OkNegotiatedContentResult<ResultModel>;
                Assert.IsFalse(contentResult.Content.Success);

                mockBrandingController.VerifyAll();
                mockFileUploadValidator.VerifyAll();
            }

            [Test]
            public void should_return_bad_request_from_post()
            {
                var mockFileUploadValidator = new Mock<IFileUploadValidator> { CallBase = true };
                mockFileUploadValidator.Setup(i => i.IsValid(It.IsAny<HttpPostedFile>())).Throws(new Exception("An error occured")).Verifiable();

                var mockBrandingController = new Mock<BrandingController>(_stubErrorHandler.Object, mockFileUploadValidator.Object, _stubGetBranding.Object, _stubSaveBranding.Object) { CallBase = true };
                mockBrandingController.Setup(i => i.GetHttpPostedFile()).Returns(ConstructHttpPostedFile(new byte[] { 1, 2, 3, 4, 5 }, "foo", "image/png")).Verifiable();

                var result = mockBrandingController.Object.Post("logo");

                Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
                mockBrandingController.VerifyAll();
                mockFileUploadValidator.VerifyAll();
            }
        }
    }
}
