using System.Collections.Generic;
using Api.Commands.User;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.User
{
    [TestFixture]
    public class GetUserTests
    {
        private static FakeDbSet<Data.User> GetMockedUserData()
        {
            var users = new List<Data.User>
            {
                new Data.User {UserId = 101, FirstName = "John", LastName = "Doe"},
                new Data.User {UserId = 102, FirstName = "Jane", LastName = "Smith"},
            };
            var userDbSet = new FakeDbSet<Data.User>();
            userDbSet.SetData(users);
            return userDbSet;
        }

        [Test]
        public void should_return_result_model_from_Execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockGetUser = new Mock<GetUser>(mockAutoRenterDatabaseContext.Object) {CallBase = true };

            var result = mockGetUser.Object.Execute(101);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(101, ((UserModel)result.Data).UserId);
            Assert.AreEqual("John", ((UserModel)result.Data).FirstName);
            Assert.IsNull(result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void should_return_result_model_from_Execute_when_user_is_not_found()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockGetUser = new Mock<GetUser>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetUser.Object.Execute(202);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("The user could not be found.", result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
