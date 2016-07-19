using Api.Models;

namespace Api.Converters
{
    public static class IncentiveGroupModelConverter
    {
        public static IncentiveGroupModel ConvertDatabaseIncentiveGroupModelToApiIncentiveGroupModel(Data.IncentiveGroup incentiveGroup)
        {
            if (incentiveGroup == null) return null;

            return new IncentiveGroupModel
            {
                IncentiveGroupId = incentiveGroup.IncentiveGroupId,
                LocationId = incentiveGroup.LocationId,
                Name = incentiveGroup.Name,
                Priority = incentiveGroup.Priority,
                StartDateTime = incentiveGroup.StartDateTime,
                EndDateTime = incentiveGroup.EndDateTime
            };
        }

        public static Data.IncentiveGroup ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(IncentiveGroupModel incentiveGroupModel)
        {
            return incentiveGroupModel == null ? null : ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(incentiveGroupModel, new Data.IncentiveGroup());
        }

        public static Data.IncentiveGroup ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(IncentiveGroupModel incentiveGroupModel, Data.IncentiveGroup incentiveGroup)
        {
            if (incentiveGroupModel == null) return null;

            incentiveGroup.IncentiveGroupId = incentiveGroupModel.IncentiveGroupId;
            incentiveGroup.LocationId = incentiveGroupModel.LocationId;
            incentiveGroup.Name = incentiveGroupModel.Name;
            incentiveGroup.Priority = incentiveGroupModel.Priority;
            incentiveGroup.StartDateTime = incentiveGroupModel.StartDateTime;
            incentiveGroup.EndDateTime = incentiveGroupModel.EndDateTime;

            return incentiveGroup;
        }
    }
}