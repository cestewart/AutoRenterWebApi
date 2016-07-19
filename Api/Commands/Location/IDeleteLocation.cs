using Api.Models;

namespace Api.Commands.Location
{
    public interface IDeleteLocation
    {
        ResultModel Execute(int locationId);
    }
}
