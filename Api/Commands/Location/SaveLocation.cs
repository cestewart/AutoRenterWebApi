using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Location
{
    public class SaveLocation : ISaveLocation
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveLocation(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(LocationModel locationModel)
        {
            return locationModel.LocationId == 0 ? CreateLocation(locationModel) : UpdateLocation(locationModel);
        }

        public virtual ResultModel CreateLocation(LocationModel locationModel)
        {
            var location = LocationModelConverter.ConvertApiLocationModelToDatabaseLocationModel(locationModel);
            _autoRenterDatabaseContext.Locations.Add(location);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(location),
                Success = true
            };
        }

        public virtual ResultModel UpdateLocation(LocationModel locationModel)
        {
            var location = _autoRenterDatabaseContext.Locations.FirstOrDefault(i => i.LocationId == locationModel.LocationId);
            LocationModelConverter.ConvertApiLocationModelToDatabaseLocationModel(locationModel, location);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = locationModel,
                Success = true
            };
        }
    }
}