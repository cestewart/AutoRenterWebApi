using System.Collections.Generic;
using Api.Commands.Vehicle;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Vehicles
{
    [TestFixture]
    public class GetVehicleTests
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
                    Vin = "V123456XXXXXX"
                },
                new Vehicle
                {
                    VehicleId = 102,
                    Vin = "V234567XXXXXX"
                }
            };
            var vehicleDbSet = new FakeDbSet<Vehicle>();
            vehicleDbSet.SetData(vehicles);
            return vehicleDbSet;
        }

        [Test]
        public void should_return_vehicle_model_from_Execute()
        {
            var mockGetVehicle = new Mock<GetVehicle>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetVehicle.Setup(i => i.GetVehicleFromDatabase(It.IsAny<int>())).Returns(new Vehicle { Vin = "V1234XXXXXXXXX" }).Verifiable();

            var result = mockGetVehicle.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("V1234XXXXXXXXX", ((VehicleModel)result.Data).Vin);
            Assert.IsNull(result.Message);
            mockGetVehicle.VerifyAll();
        }

        [Test]
        public void should_return_vehicle_model_with_error_from_Execute()
        {
            var mockGetVehicle = new Mock<GetVehicle>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetVehicle.Setup(i => i.GetVehicleFromDatabase(It.IsAny<int>())).Returns((Vehicle)null).Verifiable();

            var result = mockGetVehicle.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The vehicle could not be found.", result.Message);
            mockGetVehicle.VerifyAll();
        }

        [Test]
        public void should_return_vehicle_from_GetVehicleFromDatabase()
        {
            var locationDbSet = GetMockedVehicleData();
            locationDbSet.Setup(x => x.Include("Vehicles")).Returns(GetMockedVehicleData().Object);

            var vehiclesDbSet = GetMockedVehicleData();
            vehiclesDbSet.Setup(x => x.Include("Location")).Returns(locationDbSet.Object);

            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(vehiclesDbSet.Object).Verifiable();

            var mockGetVehicle = new Mock<GetVehicle>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetVehicle.Object.GetVehicleFromDatabase(101);

            Assert.IsInstanceOf<Vehicle>(result);
            Assert.AreEqual(101, result.VehicleId);
        }
    }
}
