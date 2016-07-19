using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.IncentiveGroup
{
    public class GetIncentiveGroup : IGetIncentiveGroup
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetIncentiveGroup(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int incentiveGroupId)
        {
            var incentiveGroup = GetIncentiveGroupFromDatabase(incentiveGroupId);

            return new ResultModel
            {
                Data = IncentiveGroupModelConverter.ConvertDatabaseIncentiveGroupModelToApiIncentiveGroupModel(incentiveGroup),
                Success = incentiveGroup != null,
                Message = incentiveGroup == null ? "The incentive group could not be found." : null
            };
        }

        public virtual Data.IncentiveGroup GetIncentiveGroupFromDatabase(int incentiveGroupId)
        {
            return _autoRenterDatabaseContext.IncentiveGroups.FirstOrDefault(i => i.IncentiveGroupId == incentiveGroupId);
        }
    }
}