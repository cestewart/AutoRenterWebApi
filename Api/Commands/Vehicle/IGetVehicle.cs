using Api.Models;

namespace Api.Commands.Vehicle
{
    public interface IGetVehicle
    {
        ResultModel Execute(int vehicleId);
    }
}
