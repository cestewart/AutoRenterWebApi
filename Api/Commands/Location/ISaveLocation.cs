using Api.Models;

namespace Api.Commands.Location
{
    public interface ISaveLocation
    {
        ResultModel Execute(LocationModel locationModel);
    }
}
