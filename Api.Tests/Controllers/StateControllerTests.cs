using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Api.Commands.State;
using Api.Controllers;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class StateControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetAllStates> _stubGetAllStates;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetAllStates = new Mock<IGetAllStates>();
        }

        [Test]
        public void should_return_ok_from_get_all_states()
        {
            var mockGetAllStates = new Mock<IGetAllStates> { CallBase = true };
            mockGetAllStates.Setup(i => i.Execute()).Returns(new ResultModel {Success = true}).Verifiable();

            var mockStateController = new Mock<StateController>(_stubErrorHandler.Object, mockGetAllStates.Object) { CallBase = true };

            var result = mockStateController.Object.Get();

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockGetAllStates.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_all_states()
        {
            var mockGetAllStates = new Mock<IGetAllStates> { CallBase = true };
            mockGetAllStates.Setup(i => i.Execute()).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockStateController = new Mock<StateController>(mockErrorHandler.Object, mockGetAllStates.Object) { CallBase = true };

            var result = mockStateController.Object.Get();

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            Assert.AreEqual("An error occured", ((BadRequestErrorMessageResult)result).Message);
            mockGetAllStates.VerifyAll();
            mockErrorHandler.VerifyAll();
        }
    }
}
