using System.Collections.Generic;
using Api.Authentication;
using Api.Commands.Login;
using Api.Converters;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Login
{
    [TestFixture]
    public class GetUserByUsernameAndPasswordTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;
        private Mock<ITokenManager> _stubTokenManager;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
            _stubTokenManager = new Mock<ITokenManager>();
        }

        private static FakeDbSet<Data.User> GetMockedUserData()
        {
            var users = new List<Data.User>
            {
                new Data.User {UserId = 101, FirstName = "John", LastName = "Doe", Username = "jdoe", HashOfPassword = HashConverter.ToHash("ThisIsMyPassword")},
                new Data.User {UserId = 102, FirstName = "Jane", LastName = "Smith", Username = "jsmith", HashOfPassword = HashConverter.ToHash("ThisIsMyGreatPassword")},
            };
            var userDbSet = new FakeDbSet<Data.User>();
            userDbSet.SetData(users);
            return userDbSet;
        }

        [Test]
        public void should_return_ResultModel_with_success_equals_true_from_Execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockGetUserByUsernameAndPassword = new Mock<GetUserByUsernameAndPassword>(mockAutoRenterDatabaseContext.Object, _stubTokenManager.Object) { CallBase = true };
            mockGetUserByUsernameAndPassword.Setup(i => i.GetUserModelWithToken(It.IsAny<Data.User>())).Returns(new UserModel()).Verifiable();

            var result = mockGetUserByUsernameAndPassword.Object.Execute(new LoginModel { Username = "jdoe", Password = "ThisIsMyPassword" });

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
            mockGetUserByUsernameAndPassword.VerifyAll();
        }

        [Test]
        public void should_return_ResultModel_with_success_equals_false_from_Execute_when_password_is_wrong()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockGetUserByUsernameAndPassword = new Mock<GetUserByUsernameAndPassword>(mockAutoRenterDatabaseContext.Object, _stubTokenManager.Object) { CallBase = true };
            mockGetUserByUsernameAndPassword.Setup(i => i.GetUserModelWithToken(It.IsAny<Data.User>())).Returns((UserModel)null).Verifiable();

            var result = mockGetUserByUsernameAndPassword.Object.Execute(new LoginModel { Username = "jdoe", Password = "ThisIsTheWrongPassword" });

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("Login failed.  Please try again.", result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
            mockGetUserByUsernameAndPassword.VerifyAll();
        }

        [Test]
        public void should_return_ResultModel_with_success_equals_false_from_Execute_when_username_is_not_found()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockGetUserByUsernameAndPassword = new Mock<GetUserByUsernameAndPassword>(mockAutoRenterDatabaseContext.Object, _stubTokenManager.Object) { CallBase = true };
            mockGetUserByUsernameAndPassword.Setup(i => i.GetUserModelWithToken(It.IsAny<Data.User>())).Returns((UserModel)null).Verifiable();

            var result = mockGetUserByUsernameAndPassword.Object.Execute(new LoginModel { Username = "gwashington", Password = "ThisIsMyPassword" });

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("Login failed.  Please try again.", result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
            mockGetUserByUsernameAndPassword.VerifyAll();
        }

        [Test]
        public void should_return_UserModel_from_GetUserModelWithToken()
        {
            var mockTokenManager = new Mock<ITokenManager> { CallBase = true };
            mockTokenManager.Setup(i => i.CreateToken(It.IsAny<Data.User>())).Returns("bearertoken");

            var mockGetUserByUsernameAndPassword = new Mock<GetUserByUsernameAndPassword>(_stubAutoRenterDatabaseContext.Object, mockTokenManager.Object) { CallBase = true };

            var result = mockGetUserByUsernameAndPassword.Object.GetUserModelWithToken(new Data.User());

            Assert.IsNotNull(result);
            Assert.AreEqual("bearertoken", result.BearerToken);
            mockTokenManager.VerifyAll();
        }

        [Test]
        public void should_return_null_from_GetUserModelWithToken()
        {
            var mockGetUserByUsernameAndPassword = new Mock<GetUserByUsernameAndPassword>(_stubAutoRenterDatabaseContext.Object, _stubTokenManager.Object) { CallBase = true };

            var result = mockGetUserByUsernameAndPassword.Object.GetUserModelWithToken(null);

            Assert.IsNull(result);
        }
    }
}
