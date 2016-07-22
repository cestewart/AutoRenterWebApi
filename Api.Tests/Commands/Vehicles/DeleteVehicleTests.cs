using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Commands.Vehicle;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Vehicles
{
    [TestFixture]
    public class DeleteVehicleTests
    {
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
        public void Execute_should_remove_vehicle()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(GetMockedVehicleData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockDeleteVehicle = new Mock<DeleteVehicle>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteVehicle.Object.Execute(101);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);

            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void Execute_should_handle_missing_vehicle()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(GetMockedVehicleData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockDeleteVehicle = new Mock<DeleteVehicle>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockDeleteVehicle.Object.Execute(202);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("The vehicle could not be found.", result.Message);

            mockAutoRenterDatabaseContext.Verify(i => i.Vehicles, Times.Once);
            mockAutoRenterDatabaseContext.Verify(i => i.SaveChanges(), Times.Never);
        }
    }
}
