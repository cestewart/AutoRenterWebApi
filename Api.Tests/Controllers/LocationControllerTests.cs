using System;
using System.Web.Http.Results;
using Api.Commands.Location;
using Api.Controllers;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class LocationControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetLocation> _stubGetLocation;
        private Mock<IGetAllLocations> _stubGetAllLocations;
        private Mock<ISaveLocation> _stubSaveLocation;
        private Mock<IDeleteLocation> _stubDeleteLocation;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetLocation = new Mock<IGetLocation>();
            _stubGetAllLocations = new Mock<IGetAllLocations>();
            _stubSaveLocation = new Mock<ISaveLocation>();
            _stubDeleteLocation = new Mock<IDeleteLocation>();
        }

        [Test]
        public void should_return_ok_from_get_all_location()
        {
            var mockGetAllLocations = new Mock<IGetAllLocations> { CallBase = true };
            mockGetAllLocations.Setup(i => i.Execute()).Returns(new ResultModel {Success = true}).Verifiable();

            var mockLocationController = new Mock<LocationController>(_stubErrorHandler.Object, _stubGetLocation.Object, mockGetAllLocations.Object, _stubSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Get();

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockGetAllLocations.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_all_locations()
        {
            var mockGetAllLocations = new Mock<IGetAllLocations> { CallBase = true };
            mockGetAllLocations.Setup(i => i.Execute()).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockLocationController = new Mock<LocationController>(mockErrorHandler.Object, _stubGetLocation.Object, mockGetAllLocations.Object, _stubSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Get();

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error occured", ((BadRequestErrorMessageResult)result).Message);
            mockGetAllLocations.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_get_by_Locationid()
        {
            var mockGetLocation = new Mock<IGetLocation> { CallBase = true };
            mockGetLocation.Setup(i => i.Execute(It.IsAny<int>())).Returns(new ResultModel {Success = true}).Verifiable();

            var mockLocationController = new Mock<LocationController>(_stubErrorHandler.Object, mockGetLocation.Object, _stubGetAllLocations.Object, _stubSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Get(101);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockGetLocation.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_by_Locationid()
        {
            var mockGetLocation = new Mock<IGetLocation> { CallBase = true };
            mockGetLocation.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockLocationController = new Mock<LocationController>(mockErrorHandler.Object, mockGetLocation.Object, _stubGetAllLocations.Object, _stubSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Get(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error occured", ((BadRequestErrorMessageResult)result).Message);
            mockGetLocation.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_post()
        {
            var resultModel = new ResultModel { Success = true, Data = new LocationModel { LocationId = 101, Name = "Location One", SiteId = "LO1" } };

            var mockSaveLocation = new Mock<ISaveLocation> { CallBase = true };
            mockSaveLocation.Setup(i => i.Execute(It.IsAny<LocationModel>())).Returns(resultModel).Verifiable();

            var mockLocationController = new Mock<LocationController>(_stubErrorHandler.Object, _stubGetLocation.Object, _stubGetAllLocations.Object, mockSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Post(new LocationModel());

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockSaveLocation.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_post()
        {
            var mockSaveLocation = new Mock<ISaveLocation> { CallBase = true };
            mockSaveLocation.Setup(i => i.Execute(It.IsAny<LocationModel>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockLocationController = new Mock<LocationController>(mockErrorHandler.Object, _stubGetLocation.Object, _stubGetAllLocations.Object, mockSaveLocation.Object, _stubDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Post(new LocationModel());

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error occured", ((BadRequestErrorMessageResult)result).Message);
            mockSaveLocation.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_delete()
        {
            var mockDeleteLocation = new Mock<IDeleteLocation> { CallBase = true };
            mockDeleteLocation.Setup(i => i.Execute(It.IsAny<int>())).Returns(new ResultModel { Success = true }).Verifiable();

            var mockLocationController = new Mock<LocationController>(_stubErrorHandler.Object, _stubGetLocation.Object, _stubGetAllLocations.Object, _stubSaveLocation.Object, mockDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Delete(101);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockDeleteLocation.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_delete()
        {
            var mockDeleteLocation = new Mock<IDeleteLocation> { CallBase = true };
            mockDeleteLocation.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockLocationController = new Mock<LocationController>(mockErrorHandler.Object, _stubGetLocation.Object, _stubGetAllLocations.Object, _stubSaveLocation.Object, mockDeleteLocation.Object) { CallBase = true };

            var result = mockLocationController.Object.Delete(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error occured", ((BadRequestErrorMessageResult)result).Message);
            mockDeleteLocation.VerifyAll();
            mockErrorHandler.VerifyAll();
        }
    }
}
