using System.Linq;
using Api.Models;
using Data;

namespace Api.Commands.Vehicle
{
    public class DeleteVehicle : IDeleteVehicle
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public DeleteVehicle(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int vehicleId)
        {
            var vehicle = _autoRenterDatabaseContext.Vehicles.FirstOrDefault(i => i.VehicleId == vehicleId);

            if (vehicle != null)
            {
                _autoRenterDatabaseContext.Vehicles.Remove(vehicle);
                _autoRenterDatabaseContext.SaveChanges();
            }

            return new ResultModel
            {
                Success = vehicle != null,
                Message = vehicle == null ? "The vehicle could not be found." : null
            };
        }
    }
}