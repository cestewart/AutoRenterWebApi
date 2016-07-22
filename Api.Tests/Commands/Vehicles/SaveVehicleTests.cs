using System.Collections.Generic;
using Api.Commands.Media;
using Api.Commands.Vehicle;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Vehicles
{
    [TestFixture]
    public class SaveVehicleTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;
        private Mock<ISaveMedia> _stubSaveMedia;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
            _stubSaveMedia = new Mock<ISaveMedia>();
        }

        private static FakeDbSet<Vehicle> GetMockedVehicleData()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle {VehicleId = 101, Vin = "V123456XXXXXXXXX"},
                new Vehicle {VehicleId = 102, Vin = "V234567XXXXXXXXXXX"},
            };
            var vehicleDbSet = new FakeDbSet<Vehicle>();
            vehicleDbSet.SetData(vehicles);
            return vehicleDbSet;
        }

        [Test]
        public void Execute_should_create_new_vehicle_record_by_calling_CreateVehicle()
        {
            var mockSaveVehicle = new Mock<SaveVehicle>(_stubAutoRenterDatabaseContext.Object, _stubSaveMedia.Object) { CallBase = true };
            mockSaveVehicle.Setup(i => i.CreateVehicle(It.IsAny<VehicleModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveVehicle.Object.Execute(new VehicleModel());

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveVehicle.Verify(i => i.CreateVehicle(It.IsAny<VehicleModel>()), Times.Once);
            mockSaveVehicle.Verify(i => i.UpdateVehicle(It.IsAny<VehicleModel>()), Times.Never);
        }

        [Test]
        public void Execute_should_update_vehicle_record_by_calling_UpdateVehicle()
        {
            var mockSaveVehicle = new Mock<SaveVehicle>(_stubAutoRenterDatabaseContext.Object, _stubSaveMedia.Object) { CallBase = true };
            mockSaveVehicle.Setup(i => i.UpdateVehicle(It.IsAny<VehicleModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveVehicle.Object.Execute(new VehicleModel { VehicleId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveVehicle.Verify(i => i.CreateVehicle(It.IsAny<VehicleModel>()), Times.Never);
            mockSaveVehicle.Verify(i => i.UpdateVehicle(It.IsAny<VehicleModel>()), Times.Once);
        }

        [Test]
        public void CreateVehicle_should_create_vehicle_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(GetMockedVehicleData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveMedia = new Mock<ISaveMedia> {CallBase = true};
            mockSaveMedia.Setup(i => i.Execute(It.IsAny<MediaModel>())).Returns(new ResultModel { Data = new MediaModel { MediaId = 303 }}).Verifiable();

            var mockSaveVehicle = new Mock<SaveVehicle>(mockAutoRenterDatabaseContext.Object, mockSaveMedia.Object) { CallBase = true };

            var result = mockSaveVehicle.Object.CreateVehicle(new VehicleModel());

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
            mockSaveMedia.VerifyAll();
        }

        [Test]
        public void UpdateVehicle_should_update_vehicle_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Vehicles).Returns(GetMockedVehicleData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveMedia = new Mock<ISaveMedia> { CallBase = true };
            mockSaveMedia.Setup(i => i.Execute(It.IsAny<MediaModel>())).Returns(new ResultModel { Data = new MediaModel { MediaId = 303 } }).Verifiable();

            var mockSaveVehicle = new Mock<SaveVehicle>(mockAutoRenterDatabaseContext.Object, mockSaveMedia.Object) { CallBase = true };

            var result = mockSaveVehicle.Object.UpdateVehicle(new VehicleModel { VehicleId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
            mockSaveMedia.VerifyAll();
        }
    }
}
