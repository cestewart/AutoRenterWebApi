using Api.Models;

namespace Api.Commands.IncentiveGroup
{
    public interface IGetIncentiveGroupsForLocation
    {
        ResultModel Execute(int locationId);
    }
}
