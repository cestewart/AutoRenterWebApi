using System.Web;
using Api.Models;

namespace Api.Converters
{
    public interface IConvertHttpRequestToVehicleModel
    {
        VehicleModel Execute(HttpRequest httpRequest);
    }
}