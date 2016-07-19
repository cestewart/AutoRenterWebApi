using Api.Models;

namespace Api.Commands.Location
{
    public interface IGetLocation
    {
        ResultModel Execute(int locationId);
    }
}
