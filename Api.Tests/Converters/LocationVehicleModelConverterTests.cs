using Api.Converters;
using Data;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]

    public class LocationVehicleModelConverterTests
    {
        public void should_return_location_vehicle_model_from_convert_database_location_model_to_api_location_vehicle_model()
        {
            var vehicle = new Vehicle
            {
                VehicleId = 101,
                Vin ="V12345XXXXXXXXX",
                Make = "Honda",
                Model = "Civic",
                Year = 2016,
                Miles = 1231,
                Color = "Blue",
                LocationId = 202,
                Location = new Location
                {
                    Name = "Indiapolis Location One"
                },
                Thumbnail = new byte [] { 0, 1, 2, 3, 4 }
            };

            var result = LocationVehicleModelConverter.ConvertDatabaseVehicleModelToApiLocationVehicleModel(vehicle);

            Assert.AreEqual(vehicle.VehicleId, result.VehicleId);
            Assert.AreEqual(vehicle.Vin, result.Vin);
            Assert.AreEqual(vehicle.Make, result.Make);
            Assert.AreEqual(vehicle.Model, result.Model);
            Assert.AreEqual(vehicle.Year, result.Year);
            Assert.AreEqual(vehicle.Miles, result.Miles);
            Assert.AreEqual(vehicle.Color, result.Color);
            Assert.AreEqual(vehicle.Location.Name, result.Location);
            Assert.AreEqual(vehicle.LocationId, result.LocationId);
            Assert.AreEqual(vehicle.Thumbnail, StreamConverter.ConvertStreamToByteArray(result.Thumbnail));
        }
    }
}
