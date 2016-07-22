using System.Collections.Generic;
using Api.Commands.User;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.User
{
    [TestFixture]
    public class SearchForUsersTests
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
        public void should_return_result_model_from_Execute()
        {
            var users = new List<Data.User>
            {
                new Data.User { UserId = 101 },
                new Data.User { UserId = 102 },
                new Data.User { UserId = 103 }
            };

            var mockSearchForUsers = new Mock<SearchForUsers>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };

            mockSearchForUsers.Setup(i => i.SearchDatabaseForUsers(It.IsAny<string>())).Returns(users).Verifiable();
            var result = mockSearchForUsers.Object.Execute("fred");

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, ((List<UserModel>)result.Data).Count);

            mockSearchForUsers.VerifyAll();
        }

        [Test]
        public void should_return_result_model_from_Execute_when_no_matching_users_are_found()
        {
            var mockSearchForUsers = new Mock<SearchForUsers>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };

            mockSearchForUsers.Setup(i => i.SearchDatabaseForUsers(It.IsAny<string>())).Returns(new List<Data.User>()).Verifiable();
            var result = mockSearchForUsers.Object.Execute("fred");

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Data);

            mockSearchForUsers.VerifyAll();
        }

        [Test]
        public void should_return_list_of_users_from_SearchDatabaseForUsers()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();

            var mockSearchForUsers = new Mock<SearchForUsers>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSearchForUsers.Object.SearchDatabaseForUsers("doe");

            Assert.AreEqual(1, result.Count);

            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
