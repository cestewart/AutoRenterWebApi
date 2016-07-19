using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Vehicle
{
    public class SaveVehicle : ISaveVehicle
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveVehicle(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(VehicleModel vehicleModel)
        {
            return vehicleModel.VehicleId <= 0 ? CreateVehicle(vehicleModel) : UpdateVehicle(vehicleModel);
        }

        public virtual ResultModel CreateVehicle(VehicleModel vehicleModel)
        {
            var vehicle = VehicleModelConverter.ConvertApiVehicleModelToDatabaseVehicleModel(vehicleModel);
            _autoRenterDatabaseContext.Vehicles.Add(vehicle);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = VehicleModelConverter.ConvertDatabaseVehicleModelToApiVehicleModel(vehicle),
                Success = true
            };
        }

        public virtual ResultModel UpdateVehicle(VehicleModel vehicleModel)
        {
            var vehicle = _autoRenterDatabaseContext.Vehicles.FirstOrDefault(i => i.VehicleId == vehicleModel.VehicleId);
            VehicleModelConverter.ConvertApiVehicleModelToDatabaseVehicleModel(vehicleModel, vehicle);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = vehicleModel,
                Success = true
            };
        }
    }
}