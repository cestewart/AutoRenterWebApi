using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Api.Commands.User;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.User
{
    [TestFixture]
    public class DeleteUserTests
    {
        private static FakeDbSet<Data.User> GetMockedUserData()
        {
            var users = new List<Data.User>
            {
                new Data.User {UserId = 101, FirstName = "John", LastName = "Doe"},
                new Data.User {UserId = 102, FirstName = "Jane", LastName = "Smith"}
            };
            var userDbSet = new FakeDbSet<Data.User>();
            userDbSet.SetData(users);
            return userDbSet;
        }

        [Test]
        public void Execute_should_remove_user()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockDeleteUser = new Mock<DeleteUser>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteUser.Object.Execute(101);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);

            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void Execute_should_handle_missing_user()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Users).Returns(GetMockedUserData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockDeleteUser = new Mock<DeleteUser>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteUser.Object.Execute(202);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("The user could not be found.", result.Message);

            mockAutoRenterDatabaseContext.Verify(i => i.Users, Times.Once);
            mockAutoRenterDatabaseContext.Verify(i => i.SaveChanges(), Times.Never);
        }
    }
}
