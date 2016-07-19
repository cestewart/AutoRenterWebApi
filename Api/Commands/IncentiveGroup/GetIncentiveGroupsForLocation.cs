using System.Collections.Generic;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.IncentiveGroup
{
    public class GetIncentiveGroupsForLocation : IGetIncentiveGroupsForLocation
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetIncentiveGroupsForLocation(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int locationId)
        {
            var incentiveGroups = GetIncentiveGroupsForLocationFromDatabase(locationId);

            return new ResultModel
            {
                Data = incentiveGroups?.Select(IncentiveGroupModelConverter.ConvertDatabaseIncentiveGroupModelToApiIncentiveGroupModel).ToList() ?? new List<IncentiveGroupModel>(),
                Success = true
            };
        }

        public virtual List<Data.IncentiveGroup> GetIncentiveGroupsForLocationFromDatabase(int locationId)
        {
            return _autoRenterDatabaseContext.IncentiveGroups.Where(i => i.LocationId == locationId).ToList();
        }
    }
}