using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Api.Commands.IncentiveGroup;
using Api.Controllers;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class IncentiveGroupControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetIncentiveGroup> _stubGetIncentiveGroup;
        private Mock<IGetIncentiveGroupsForLocation> _stubGetIncentiveGroupsForLocation;
        private Mock<ISaveIncentiveGroup> _stubSaveIncentiveGroup;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetIncentiveGroup = new Mock<IGetIncentiveGroup>();
            _stubGetIncentiveGroupsForLocation = new Mock<IGetIncentiveGroupsForLocation>();
            _stubSaveIncentiveGroup = new Mock<ISaveIncentiveGroup>();
        }

        [Test]
        public void should_return_ok_from_get_incentive_groups_for_location()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new List<IncentiveGroupModel>
                {
                    new IncentiveGroupModel
                    {
                        IncentiveGroupId = 101,
                        Name = "IncentiveGroup One"
                    },
                    new IncentiveGroupModel
                    {
                        IncentiveGroupId = 102,
                        Name = "IncentiveGroup Two"
                    }
                }
            };

            var mockGetIncentiveGroupsForLocation = new Mock<IGetIncentiveGroupsForLocation> { CallBase = true };
            mockGetIncentiveGroupsForLocation.Setup(i => i.Execute(It.IsAny<int>())).Returns(resultModel).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(_stubErrorHandler.Object, _stubGetIncentiveGroup.Object, mockGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.GetIncentiveGroupsForLocation(201);

            var contentResult = result as OkNegotiatedContentResult<ResultModel>;
            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            var data = (List<IncentiveGroupModel>)contentResult.Content.Data;
            Assert.AreEqual(2, data.Count);
            mockGetIncentiveGroupsForLocation.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_all_locations()
        {
            var mockGetIncentiveGroupsForLocation = new Mock<IGetIncentiveGroupsForLocation> { CallBase = true };
            mockGetIncentiveGroupsForLocation.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(mockErrorHandler.Object, _stubGetIncentiveGroup.Object, mockGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.GetIncentiveGroupsForLocation(201);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockGetIncentiveGroupsForLocation.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_get()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data =
                    new IncentiveGroupModel
                    {
                        IncentiveGroupId = 101,
                        Name = "IncentiveGroup One"
                    }
            };

            var mockGetIncentiveGroup = new Mock<IGetIncentiveGroup> { CallBase = true };
            mockGetIncentiveGroup.Setup(i => i.Execute(It.IsAny<int>())).Returns(resultModel).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(_stubErrorHandler.Object, mockGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.Get(101);

            var contentResult = result as OkNegotiatedContentResult<ResultModel>;
            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            var data = (IncentiveGroupModel)contentResult.Content.Data;
            Assert.AreEqual(101, data.IncentiveGroupId);
            mockGetIncentiveGroup.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get()
        {
            var mockGetIncentiveGroup = new Mock<IGetIncentiveGroup> { CallBase = true };
            mockGetIncentiveGroup.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(mockErrorHandler.Object, mockGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.Get(201);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockGetIncentiveGroup.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_save()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new IncentiveGroupModel
                {
                    IncentiveGroupId = 101,
                    Name = "IncentiveGroup One"
                }
            };

            var mockSaveIncentiveGroup = new Mock<ISaveIncentiveGroup> { CallBase = true };
            mockSaveIncentiveGroup.Setup(i => i.Execute(It.IsAny<IncentiveGroupModel>())).Returns(resultModel).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(_stubErrorHandler.Object, _stubGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, mockSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.Save(new IncentiveGroupModel());

            var contentResult = result as OkNegotiatedContentResult<ResultModel>;
            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            var data = (IncentiveGroupModel)contentResult.Content.Data;
            Assert.AreEqual(101, data.IncentiveGroupId);
            Assert.AreEqual("IncentiveGroup One", data.Name);
            mockSaveIncentiveGroup.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_save()
        {
            var mockSaveIncentiveGroup = new Mock<ISaveIncentiveGroup> { CallBase = true };
            mockSaveIncentiveGroup.Setup(i => i.Execute(It.IsAny<IncentiveGroupModel>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(mockErrorHandler.Object, _stubGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, mockSaveIncentiveGroup.Object) { CallBase = true };

            var result = mockIncentiveGroupController.Object.Save(new IncentiveGroupModel());

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockSaveIncentiveGroup.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void post_should_call_save()
        {
            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(_stubErrorHandler.Object, _stubGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };
            mockIncentiveGroupController.Setup(i => i.Save(It.IsAny<IncentiveGroupModel>())).Verifiable();

            mockIncentiveGroupController.Object.Post(new IncentiveGroupModel());

            mockIncentiveGroupController.VerifyAll();
        }

        [Test]
        public void put_should_call_save()
        {
            var mockIncentiveGroupController = new Mock<IncentiveGroupController>(_stubErrorHandler.Object, _stubGetIncentiveGroup.Object, _stubGetIncentiveGroupsForLocation.Object, _stubSaveIncentiveGroup.Object) { CallBase = true };
            mockIncentiveGroupController.Setup(i => i.Save(It.IsAny<IncentiveGroupModel>())).Verifiable();

            mockIncentiveGroupController.Object.Put(new IncentiveGroupModel());

            mockIncentiveGroupController.VerifyAll();
        }
    }
}
