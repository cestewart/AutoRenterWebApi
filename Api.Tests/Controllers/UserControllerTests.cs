using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Api.Commands.User;
using Api.Controllers;
using Api.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetUser> _stubGetUser;
        private Mock<ISearchForUsers> _stubSearchForUsers;
        private Mock<ISaveUser> _stubSaveUser;
        private Mock<IDeleteUser> _stubDeleteUser;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetUser = new Mock<IGetUser>();
            _stubSearchForUsers = new Mock<ISearchForUsers>();
            _stubSaveUser = new Mock<ISaveUser>();
            _stubDeleteUser = new Mock<IDeleteUser>();
        }

        [Test]
        public void should_return_ok_from_get_by_userid()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new UserModel
                {
                    UserId = 101,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };

            var mockGetUser = new Mock<IGetUser> { CallBase = true };
            mockGetUser.Setup(i => i.Execute(It.IsAny<int>())).Returns(resultModel).Verifiable();

            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, mockGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Get(101);
            var contentResult = result as OkNegotiatedContentResult<ResultModel>;

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);

            var data = (UserModel)contentResult.Content.Data;

            Assert.AreEqual(101, data.UserId);
            Assert.AreEqual("John", data.FirstName);
            Assert.AreEqual("Doe", data.LastName);
            mockGetUser.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_by_userid()
        {
            var mockGetUser = new Mock<IGetUser> { CallBase = true };
            mockGetUser.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockUserController = new Mock<UserController>(mockErrorHandler.Object, mockGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Get(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockGetUser.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_get_by_search_term()
        {
            var resultModel = new ResultModel
            {
                Success = true,
                Data = new List<UserModel>
                {
                    new UserModel
                    {
                        UserId = 101,
                        FirstName = "John",
                        LastName = "Doe"
                    },
                    new UserModel
                    {
                        UserId = 102,
                        FirstName = "Jane",
                        LastName = "Smith"
                    }
                }
            };

            var mockSearchForUsers = new Mock<ISearchForUsers> { CallBase = true };
            mockSearchForUsers.Setup(i => i.Execute(It.IsAny<string>())).Returns(resultModel).Verifiable();

            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, _stubGetUser.Object, mockSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Get("doe");
            var contentResult = result as OkNegotiatedContentResult<ResultModel>;

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);

            var data = (List<UserModel>)contentResult.Content.Data;

            Assert.AreEqual(2, data.Count);
            mockSearchForUsers.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_get_by_search_term()
        {
            var mockSearchForUsers = new Mock<ISearchForUsers> { CallBase = true };
            mockSearchForUsers.Setup(i => i.Execute(It.IsAny<string>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockUserController = new Mock<UserController>(mockErrorHandler.Object, _stubGetUser.Object, mockSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Get("doe");

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockSearchForUsers.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_save()
        {
            var resultModel = new ResultModel { Success = true, Data = new UserModel { UserId = 101, FirstName = "John", LastName = "Doe" } };

            var mockSaveUser = new Mock<ISaveUser> { CallBase = true };
            mockSaveUser.Setup(i => i.Execute(It.IsAny<UserModel>())).Returns(resultModel).Verifiable();

            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, mockSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Save(new UserModel());
            var contentResult = result as OkNegotiatedContentResult<ResultModel>;

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);

            var data = (UserModel)contentResult.Content.Data;

            Assert.AreEqual(101, data.UserId);
            Assert.AreEqual("John", data.FirstName);
            Assert.AreEqual("Doe", data.LastName);
            mockSaveUser.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_save()
        {
            var mockSaveUser = new Mock<ISaveUser> { CallBase = true };
            mockSaveUser.Setup(i => i.Execute(It.IsAny<UserModel>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockUserController = new Mock<UserController>(mockErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, mockSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Save(new UserModel());

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockSaveUser.VerifyAll();
            mockErrorHandler.VerifyAll();
        }

        [Test]
        public void put_should_call_save()
        {
            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };
            mockUserController.Setup(i => i.Save(It.IsAny<UserModel>())).Verifiable();

            mockUserController.Object.Put(new UserModel());

            mockUserController.VerifyAll();
        }

        [Test]
        public void post_should_call_save()
        {
            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, _stubDeleteUser.Object) { CallBase = true };
            mockUserController.Setup(i => i.Save(It.IsAny<UserModel>())).Verifiable();

            mockUserController.Object.Post(new UserModel());

            mockUserController.VerifyAll();
        }

        [Test]
        public void should_return_ok_from_delete()
        {
            var mockDeleteUser = new Mock<IDeleteUser> { CallBase = true };
            mockDeleteUser.Setup(i => i.Execute(It.IsAny<int>())).Returns(new ResultModel { Success = true }).Verifiable();

            var mockUserController = new Mock<UserController>(_stubErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, mockDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Delete(101);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ResultModel>>(result);
            mockDeleteUser.VerifyAll();
        }

        [Test]
        public void should_return_bad_request_from_delete()
        {
            var mockDeleteUser = new Mock<IDeleteUser> { CallBase = true };
            mockDeleteUser.Setup(i => i.Execute(It.IsAny<int>())).Throws(new Exception("An error occured")).Verifiable();

            var mockErrorHandler = new Mock<IErrorHandler> { CallBase = true };
            mockErrorHandler.Setup(i => i.LogError(It.IsAny<Exception>())).Verifiable();

            var mockUserController = new Mock<UserController>(mockErrorHandler.Object, _stubGetUser.Object, _stubSearchForUsers.Object, _stubSaveUser.Object, mockDeleteUser.Object) { CallBase = true };

            var result = mockUserController.Object.Delete(101);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
            mockDeleteUser.VerifyAll();
            mockErrorHandler.VerifyAll();
        }
    }
}
