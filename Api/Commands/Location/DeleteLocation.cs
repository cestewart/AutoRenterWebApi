using System.Linq;
using Api.Models;
using Data;

namespace Api.Commands.Location
{
    public class DeleteLocation : IDeleteLocation
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public DeleteLocation(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int locationId)
        {
            var location = _autoRenterDatabaseContext.Locations.FirstOrDefault(i => i.LocationId == locationId);

            // ReSharper disable once InvertIf
            if (location != null)
            {
                _autoRenterDatabaseContext.Locations.Remove(location);
                _autoRenterDatabaseContext.SaveChanges();
            }

            return new ResultModel
            {
                Success = location != null,
                Message = location == null ? "The location could not be found." : null
            };
        }
    }
}