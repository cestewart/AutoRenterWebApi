using System.Collections.Generic;
using Api.Commands.Location;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Location
{
    [TestFixture]
    public partial class GetAllLocationsTests
    {
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
        public void should_return_list_of_locations_from_Execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Locations).Returns(GetMockedLocationData().Object).Verifiable();

            var mockGetAllLocations = new Mock<GetAllLocations>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetAllLocations.Object.Execute();

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, ((List<LocationModel>)result.Data).Count);
            Assert.IsNull(result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
