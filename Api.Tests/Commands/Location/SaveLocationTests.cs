using System.Collections.Generic;
using Api.Commands.Location;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Location
{
    [TestFixture]
    public class SaveLocationTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

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
        public void execute_should_create_new_location_record_by_calling_create_location()
        {
            var mockSaveLocation = new Mock<SaveLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveLocation.Setup(i => i.CreateLocation(It.IsAny<LocationModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveLocation.Object.Execute(new LocationModel());

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveLocation.Verify(i => i.CreateLocation(It.IsAny<LocationModel>()), Times.Once);
            mockSaveLocation.Verify(i => i.UpdateLocation(It.IsAny<LocationModel>()), Times.Never);
        }

        [Test]
        public void execute_should_update_location_record_by_calling_update_location()
        {
            var mockSaveLocation = new Mock<SaveLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveLocation.Setup(i => i.UpdateLocation(It.IsAny<LocationModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveLocation.Object.Execute(new LocationModel { LocationId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveLocation.Verify(i => i.CreateLocation(It.IsAny<LocationModel>()), Times.Never);
            mockSaveLocation.Verify(i => i.UpdateLocation(It.IsAny<LocationModel>()), Times.Once);
        }

        [Test]
        public void create_location_should_create_Location_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(GetMockedLocationData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveLocation = new Mock<SaveLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveLocation.Object.CreateLocation(new LocationModel());

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void create_location_should_update_location_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(GetMockedLocationData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveLocation = new Mock<SaveLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveLocation.Object.UpdateLocation(new LocationModel { LocationId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
