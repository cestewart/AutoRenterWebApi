using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.IncentiveGroup
{
    public class SaveIncentiveGroup : ISaveIncentiveGroup
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveIncentiveGroup(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(IncentiveGroupModel incentiveGroupModel)
        {
            return incentiveGroupModel.IncentiveGroupId == 0 ? CreateIncentiveGroup(incentiveGroupModel) : UpdateIncentiveGroup(incentiveGroupModel);
        }

        public virtual ResultModel CreateIncentiveGroup(IncentiveGroupModel incentiveGroupModel)
        {
            var incentiveGroup = IncentiveGroupModelConverter.ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(incentiveGroupModel);
            _autoRenterDatabaseContext.IncentiveGroups.Add(incentiveGroup);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = IncentiveGroupModelConverter.ConvertDatabaseIncentiveGroupModelToApiIncentiveGroupModel(incentiveGroup),
                Success = true
            };
        }

        public virtual ResultModel UpdateIncentiveGroup(IncentiveGroupModel incentiveGroupModel)
        {
            var incentiveGroup = _autoRenterDatabaseContext.IncentiveGroups.FirstOrDefault(i => i.IncentiveGroupId == incentiveGroupModel.IncentiveGroupId);
            IncentiveGroupModelConverter.ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(incentiveGroupModel, incentiveGroup);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = incentiveGroupModel,
                Success = true
            };
        }
    }
}