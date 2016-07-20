using Api.Converters;
using Api.Models;
using Data;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class VehicleModelConverterTests
    {
        [Test]
        public void should_return_vehicle_model_from_convert_database_vehicle_model_to_api_vehicle_model()
        {
            var vehicle = new Vehicle
            {
                VehicleId = 101,
                Vin = "123456XXXX",
                Model = "Civic",
                Year = 2016,
                Miles = 12329,
                Color = "Blue",
                RentToOwn = false,
                Make = "Honda",
                LocationId = 10,
                Location = new Location
                {
                    LocationId = 101,
                    Name = "Indpls One",
                    City = "Indy",
                    SiteId = "IDX",
                    StateId = 1,
                },
                MediaId = 707
            };

            var result = VehicleModelConverter.ConvertDatabaseVehicleModelToApiVehicleModel(vehicle);

            Assert.AreEqual(vehicle.VehicleId, result.VehicleId);
            Assert.AreEqual(vehicle.Vin, result.Vin);
            Assert.AreEqual(vehicle.Model, result.Model);
            Assert.AreEqual(vehicle.Year, result.Year);
            Assert.AreEqual(vehicle.Miles, result.Miles);
            Assert.AreEqual(vehicle.Color, result.Color);
            Assert.AreEqual(vehicle.RentToOwn, result.RentToOwn);
            Assert.AreEqual(vehicle.Make, result.Make);
            Assert.AreEqual(vehicle.LocationId, result.LocationId);
            Assert.AreEqual(vehicle.Location.Name, result.Location.Name);
            Assert.AreEqual(vehicle.Location.City, result.Location.City);
            Assert.AreEqual(vehicle.Location.SiteId, result.Location.SiteId);
            Assert.AreEqual(vehicle.MediaId, result.MediaId);
        }

        [Test]
        public void should_return_vehicle_model_from_convert_database_vehicle_model_to_api_vehicle_model_without_location()
        {
            var vehicle = new Vehicle
            {
                VehicleId = 101,
                Vin = "123456XXXX",
                Model = "Civic",
                Year = 2016,
                Miles = 12329,
                Color = "Blue",
                RentToOwn = false,
                Make = "Honda",
                LocationId = 10,
                MediaId = 707
            };

            var result = VehicleModelConverter.ConvertDatabaseVehicleModelToApiVehicleModel(vehicle);

            Assert.AreEqual(vehicle.VehicleId, result.VehicleId);
            Assert.AreEqual(vehicle.Vin, result.Vin);
            Assert.AreEqual(vehicle.Model, result.Model);
            Assert.AreEqual(vehicle.Year, result.Year);
            Assert.AreEqual(vehicle.Miles, result.Miles);
            Assert.AreEqual(vehicle.Color, result.Color);
            Assert.AreEqual(vehicle.RentToOwn, result.RentToOwn);
            Assert.AreEqual(vehicle.Make, result.Make);
            Assert.AreEqual(vehicle.LocationId, result.LocationId);
            Assert.AreEqual(vehicle.MediaId, result.MediaId);
        }

        [Test]
        public void should_return_database_vehicle_model_from_convert_api_vehicle_model_to_database_vehicle_model()
        {
            var vehicleModel = new VehicleModel
            {
                Vin = "123456XXXX",
                Model = "Civic",
                Year = 2016,
                Miles = 12329,
                Color = "Blue",
                RentToOwn = false,
                Make = "Honda",
                LocationId = 10,
                MediaId = 707
            };

            var result = VehicleModelConverter.ConvertApiVehicleModelToDatabaseVehicleModel(vehicleModel);

            Assert.AreEqual(vehicleModel.Vin, result.Vin);
            Assert.AreEqual(vehicleModel.Model, result.Model);
            Assert.AreEqual(vehicleModel.Year, result.Year);
            Assert.AreEqual(vehicleModel.Miles, result.Miles);
            Assert.AreEqual(vehicleModel.Color, result.Color);
            Assert.AreEqual(vehicleModel.RentToOwn, result.RentToOwn);
            Assert.AreEqual(vehicleModel.Make, result.Make);
            Assert.AreEqual(vehicleModel.LocationId, result.LocationId);
            Assert.AreEqual(vehicleModel.MediaId, result.MediaId);
        }

        [Test]
        public void should_return_updated_database_vehicle_model_from_convert_api_vehicle_model_to_database_vehicle_model()
        {
            var vehicleModel = new VehicleModel
            {
                Vin = "123456XXXX",
                Model = "Civic",
                Year = 2016,
                Miles = 12329,
                Color = "Blue",
                RentToOwn = false,
                Make = "Honda",
                LocationId = 10,
                MediaId = 707
            };

            var vehicle = new Vehicle
            {
                Vin = "bbbbbbbbbbbbb",
                Model = "cccccccc",
                Year = 1901,
                Miles = 44444,
                Color = "Red",
                RentToOwn = true,
                Make = "zzzzz",
                LocationId = 11,
                MediaId = 707,
            };

            var result = VehicleModelConverter.ConvertApiVehicleModelToDatabaseVehicleModel(vehicleModel, vehicle);

            Assert.AreEqual(vehicleModel.Vin, result.Vin);
            Assert.AreEqual(vehicleModel.Model, result.Model);
            Assert.AreEqual(vehicleModel.Year, result.Year);
            Assert.AreEqual(vehicleModel.Miles, result.Miles);
            Assert.AreEqual(vehicleModel.Color, result.Color);
            Assert.AreEqual(vehicleModel.RentToOwn, result.RentToOwn);
            Assert.AreEqual(vehicleModel.Make, result.Make);
            Assert.AreEqual(vehicleModel.LocationId, result.LocationId);
            Assert.AreEqual(vehicleModel.MediaId, result.MediaId);
        }
    }
}
