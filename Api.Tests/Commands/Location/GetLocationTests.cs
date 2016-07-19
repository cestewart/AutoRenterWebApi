using System.Collections.Generic;
using Api.Commands.Location;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Location
{
    [TestFixture]
    public class GetLocationTests
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
                new Data.Location
                {
                    LocationId = 101,
                    Name = "Location One",
                    City = "Chicago",
                    SiteId = "LOC",
                    StateId = 101,
                    State = new Data.State {Name = "Illinois", Abbreviation = "IL"}
                },
                new Data.Location
                {
                    LocationId = 102,
                    Name = "Location Indy",
                    City = "Indpls",
                    SiteId = "LII",
                    StateId = 102,
                    State = new Data.State {Name = "Indiana", Abbreviation = "IN"}
                }
            };
            var locationDbSet = new FakeDbSet<Data.Location>();
            locationDbSet.SetData(locations);
            return locationDbSet;
        }

        [Test]
        public void should_return_location_from_get_location_from_database()
        {
            var stateDbSet = GetMockedLocationData();
            stateDbSet.Setup(x => x.Include("Vehicles")).Returns(GetMockedLocationData().Object);

            var locationsDbSet = GetMockedLocationData();
            locationsDbSet.Setup(x => x.Include("State")).Returns(stateDbSet.Object);

            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(locationsDbSet.Object).Verifiable();

            var mockGetLocation = new Mock<GetLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetLocation.Object.GetLocationFromDatabase(101);

            Assert.IsInstanceOf<Data.Location>(result);
            Assert.AreEqual(101, result.LocationId);
        }

        [Test]
        public void should_return_location_model_from_execute()
        {
            var mockGetLocation = new Mock<GetLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetLocation.Setup(i => i.GetLocationFromDatabase(It.IsAny<int>())).Returns(new Data.Location { Name = "Location Three" }).Verifiable();

            var result = mockGetLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Location Three", ((LocationModel)result.Data).Name);
            Assert.IsNull(result.Message);
            mockGetLocation.VerifyAll();
        }

        [Test]
        public void should_return_location_model_with_error_from_execute()
        {
            var mockGetLocation = new Mock<GetLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetLocation.Setup(i => i.GetLocationFromDatabase(It.IsAny<int>())).Returns((Data.Location)null).Verifiable();

            var result = mockGetLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The location could not be found.", result.Message);
            mockGetLocation.VerifyAll();
        }
    }
}
