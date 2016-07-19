using Api.Models;

namespace Api.Commands.IncentiveGroup
{
    public interface ISaveIncentiveGroup
    {
        ResultModel Execute(IncentiveGroupModel incentiveGroupModel);
    }
}
