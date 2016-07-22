using System.Collections.Generic;
using Api.Commands.Location;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Location
{
    [TestFixture]
    public class DeleteLocationTests
    {
        private static FakeDbSet<Data.Location> GetMockedLocationData()
        {
            var locations = new List<Data.Location>
            {
                new Data.Location {LocationId = 101, Name = "Location One"},
                new Data.Location {LocationId = 102, Name = "Location Two"},
            };
            var locationDbSet = new FakeDbSet<Data.Location>();
            locationDbSet.SetData(locations);
            return locationDbSet;
        }

        [Test]
        public void Execute_should_delete_location()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(GetMockedLocationData().Object).Verifiable();

            var mockDeleteLocation = new Mock<DeleteLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
        }

        [Test]
        public void Execute_should_handle_error()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(GetMockedLocationData().Object).Verifiable();

            var mockDeleteLocation = new Mock<DeleteLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteLocation.Object.Execute(303);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The location could not be found.", result.Message);
        }
    }
}
