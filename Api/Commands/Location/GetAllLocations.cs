using System.Collections.Generic;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Location
{
    public class GetAllLocations : IGetAllLocations
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetAllLocations(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute()
        {
            var locations = _autoRenterDatabaseContext.Locations;

            return new ResultModel
            {
                Data = locations?.Select(LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel).ToList() ?? new List<LocationModel>(),
                Success = true
            };
        }
    }
}