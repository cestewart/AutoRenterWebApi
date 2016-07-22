using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Vehicle
{
    public class GetVehicle : IGetVehicle
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetVehicle(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int vehicleId)
        {
            var vehicle = GetVehicleFromDatabase(vehicleId);

            return new ResultModel
            {
                Data = VehicleModelConverter.ConvertDatabaseVehicleModelToApiVehicleModel(vehicle),
                Success = vehicle != null,
                Message = vehicle == null ? "The vehicle could not be found." : null
            };
        }

        public virtual Data.Vehicle GetVehicleFromDatabase(int vehicleId)
        {
            return _autoRenterDatabaseContext.Vehicles.Include("Location").FirstOrDefault(i => i.VehicleId == vehicleId);
        }
    }
}