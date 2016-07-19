using Api.Models;

namespace Api.Commands.Vehicle
{
    public interface IDeleteVehicle
    {
        ResultModel Execute(int vehicleId);
    }
}
