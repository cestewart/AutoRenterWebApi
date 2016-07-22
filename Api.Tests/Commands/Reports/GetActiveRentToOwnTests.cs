using System.Collections.Generic;
using Api.Commands.Reports;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Reports
{
    [TestFixture]
    public class GetActiveRentToOwnTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

        private static FakeDbSet<Vehicle> GetMockedVehicleData()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleId = 101,
                    Vin = "V123456XXXXXX",
                    RentToOwn = true
                },
                new Vehicle
                {
                    VehicleId = 102,
                    Vin = "V234567XXXXXX",
                    RentToOwn = true
                },
                new Vehicle
                {
                    VehicleId = 103,
                    Vin = "V556677XXXXXX",
                    RentToOwn = false
                }
            };
            var vehicleDbSet = new FakeDbSet<Vehicle>();
            vehicleDbSet.SetData(vehicles);
            return vehicleDbSet;
        }

        [Test]
        public void should_return_media_from_Execute()
        {
            var locationDbSet = GetMockedVehicleData();
            locationDbSet.Setup(x => x.Include("Vehicles")).Returns(GetMockedVehicleData().Object);

            var vehiclesDbSet = GetMockedVehicleData();
            vehiclesDbSet.Setup(x => x.Include("Location")).Returns(locationDbSet.Object);

            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(vehiclesDbSet.Object).Verifiable();

            var mockGetActiveRentToOwn = new Mock<GetActiveRentToOwn>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetActiveRentToOwn.Object.Execute();

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.AreEqual(2, ((List<VehicleModel>)result.Data).Count);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
