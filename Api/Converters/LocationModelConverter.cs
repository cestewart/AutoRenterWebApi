using Api.Models;

namespace Api.Converters
{
    public static class LocationModelConverter
    {
        public static LocationModel ConvertDatabaseLocationModelToApiLocationModel(Data.Location location)
        {
            if (location == null) return null;

            return new LocationModel
            {
                LocationId = location.LocationId,
                Name = location.Name,
                SiteId = location.SiteId,
                City = location.City,
                StateId = location.StateId,
                State = location.State != null ? location.State.Name : "Unknown",
                StateAbbreviation = location.State != null ? location.State.Abbreviation : "NA",
                VehicleCount = location.Vehicles?.Count ?? 0
            };
        }

        public static Data.Location ConvertApiLocationModelToDatabaseLocationModel(LocationModel locationModel)
        {
            return locationModel == null ? null : ConvertApiLocationModelToDatabaseLocationModel(locationModel, new Data.Location());
        }

        public static Data.Location ConvertApiLocationModelToDatabaseLocationModel(LocationModel locationModel, Data.Location location)
        {
            if (locationModel == null) return null;

            location.Name = locationModel.Name;
            location.SiteId = locationModel.SiteId;
            location.City = locationModel.City;
            location.StateId = locationModel.StateId;

            return location;
        }
    }
}