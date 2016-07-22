using System.Linq;
using Api.Commands.Media;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Vehicle
{
    public class SaveVehicle : ISaveVehicle
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;
        private readonly ISaveMedia _saveMedia;

        public SaveVehicle(AutoRenterDatabaseContext autoRenterDatabaseContext, ISaveMedia saveMedia)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
            _saveMedia = saveMedia;
        }

        public ResultModel Execute(VehicleModel vehicleModel)
        {
            return vehicleModel.VehicleId <= 0 ? CreateVehicle(vehicleModel) : UpdateVehicle(vehicleModel);
        }

        public virtual ResultModel CreateVehicle(VehicleModel vehicleModel)
        {
            var result = _saveMedia.Execute(vehicleModel.Image);
            vehicleModel.MediaId = ((MediaModel)result.Data).MediaId;
            return CreateVehicleDatabaseRecord(vehicleModel);
        }

        public virtual ResultModel CreateVehicleDatabaseRecord(VehicleModel vehicleModel)
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
            var result = _saveMedia.Execute(vehicleModel.Image);
            vehicleModel.MediaId = ((MediaModel)result.Data).MediaId;
            return UpdateVehicleDatabaseRecord(vehicleModel);
        }

        private ResultModel UpdateVehicleDatabaseRecord(VehicleModel vehicleModel)
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