
using Api.Models;
using Data;

namespace Api.Converters
{
    public static class LocationVehicleModelConverter
    {


        public static LocationVehicleModel ConvertDatabaseVehicleModelToApiLocationVehicleModel(Vehicle vehicle)
        {
            if (vehicle == null) return null;

            return new LocationVehicleModel
            {
                VehicleId = vehicle.VehicleId,
                Vin = vehicle.Vin,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Miles = vehicle.Miles,
                Color = vehicle.Color,
                Location = vehicle.Location.Name,
                LocationId = vehicle.LocationId,
                Thumbnail = StreamConverter.ConvertByteArrayToStream(vehicle.Thumbnail)
            };
        }
    }
}