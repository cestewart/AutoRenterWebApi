using System.Collections.Generic;
using Api.Commands.User;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.User
{
    [TestFixture]
    public class SaveUserTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

        private static FakeDbSet<Data.User> GetMockedUserData()
        {
            var users = new List<Data.User>
            {
                new Data.User {UserId = 101, FirstName = "John", LastName = "Doe", Username = "jdoe", Email = "jdoe@gmail.com"},
                new Data.User {UserId = 102, FirstName = "Jane", LastName = "Smith", Username = "jsmith", Email = "jsmith@gmail.com"}
            };
            var userDbSet = new FakeDbSet<Data.User>();
            userDbSet.SetData(users);
            return userDbSet;
        }

        [Test]
        public void execute_should_create_new_user_record_by_calling_create_user()
        {
            var mockSaveUser = new Mock<SaveUser>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveUser.Setup(i => i.CreateUser(It.IsAny<UserModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveUser.Object.Execute(new UserModel());

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveUser.Verify(i => i.CreateUser(It.IsAny<UserModel>()), Times.Once);
            mockSaveUser.Verify(i => i.UpdateUser(It.IsAny<UserModel>()), Times.Never);
        }

        [Test]
        public void execute_should_update_user_record_by_calling_update_user()
        {
            var mockSaveUser = new Mock<SaveUser>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveUser.Setup(i => i.UpdateUser(It.IsAny<UserModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveUser.Object.Execute(new UserModel { UserId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveUser.Verify(i => i.CreateUser(It.IsAny<UserModel>()), Times.Never);
            mockSaveUser.Verify(i => i.UpdateUser(It.IsAny<UserModel>()), Times.Once);
        }

        [Test]
        public void create_user_should_create_user_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveUser = new Mock<SaveUser>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveUser.Object.CreateUser(new UserModel());

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void create_user_should_update_user_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveUser = new Mock<SaveUser>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveUser.Object.UpdateUser(new UserModel { UserId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
