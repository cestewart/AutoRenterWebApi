using Api.Models;

namespace Api.Commands.IncentiveGroup
{
    public interface IGetIncentiveGroup
    {
        ResultModel Execute(int incentiveGroupId);
    }
}
