﻿using Api.Models;

namespace Api.Converters
{
    public static class VehicleModelConverter
    {
        public static VehicleModel ConvertDatabaseVehicleModelToApiVehicleModel(Data.Vehicle vehicle)
        {
            if (vehicle == null) return null;

            return new VehicleModel
            {
                VehicleId = vehicle.VehicleId,
                Vin = vehicle.Vin,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Miles = vehicle.Miles,
                Color = vehicle.Color,
                RentToOwn = vehicle.RentToOwn,
                Make = vehicle.Make,
                Thumbnail = StreamConverter.ConvertByteArrayToStream(vehicle.Thumbnail),
                Image = StreamConverter.ConvertByteArrayToStream(vehicle.Image),
                LocationId = vehicle.LocationId,
                Location = vehicle.Location != null ? LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(vehicle.Location) : null,
            };
        }

        public static Data.Vehicle ConvertApiVehicleModelToDatabaseVehicleModel(VehicleModel vehicleModel)
        {
            return vehicleModel == null ? null : ConvertApiVehicleModelToDatabaseVehicleModel(vehicleModel, new Data.Vehicle());
        }

        public static Data.Vehicle ConvertApiVehicleModelToDatabaseVehicleModel(VehicleModel vehicleModel, Data.Vehicle vehicle)
        {
            if (vehicleModel == null) return null;

            vehicle.Vin = vehicleModel.Vin;
            vehicle.Model = vehicleModel.Model;
            vehicle.Year = vehicleModel.Year;
            vehicle.Miles = vehicleModel.Miles;
            vehicle.Color = vehicleModel.Color;
            vehicle.RentToOwn = vehicleModel.RentToOwn;
            vehicle.Make = vehicleModel.Make;
            vehicle.Thumbnail = StreamConverter.ConvertStreamToByteArray(vehicleModel.Thumbnail);
            vehicle.Image = StreamConverter.ConvertStreamToByteArray(vehicleModel.Image);
            vehicle.LocationId = vehicleModel.LocationId;

            return vehicle;
        }
    }
}