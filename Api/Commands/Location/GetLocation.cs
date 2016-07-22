using System.Data.Entity;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Location
{
    public class GetLocation : IGetLocation
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetLocation(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int locationId)
        {
            var location = GetLocationFromDatabase(locationId);

            return new ResultModel
            {
                Data = LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(location),
                Success = location != null,
                Message = location == null ? "The location could not be found." : null
            };
        }

        public virtual Data.Location GetLocationFromDatabase(int locationId)
        {
            return _autoRenterDatabaseContext.Locations.Include("State").Include("Vehicles").FirstOrDefault(i => i.LocationId == locationId);
        }
    }
}