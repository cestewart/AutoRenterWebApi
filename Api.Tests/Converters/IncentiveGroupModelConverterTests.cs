using System;
using Api.Converters;
using Api.Models;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class IncentiveGroupModelConverterTests
    {
        [Test]
        public void should_return_incentive_group_model_from_convert_database_incentive_group_model_to_api_incentive_group_model()
        {
            var incentiveGroup = new Data.IncentiveGroup
            {
                IncentiveGroupId = 101,
                LocationId = 202,
                Name = "IncentiveGroup One",
                Priority = 1, 
                StartDateTime = new DateTime(2016, 6, 1),
                EndDateTime = null
            };

            var result = IncentiveGroupModelConverter.ConvertDatabaseIncentiveGroupModelToApiIncentiveGroupModel(incentiveGroup);

            Assert.AreEqual(incentiveGroup.IncentiveGroupId, result.IncentiveGroupId);
            Assert.AreEqual(incentiveGroup.LocationId, result.LocationId);
            Assert.AreEqual(incentiveGroup.Name, result.Name);
            Assert.AreEqual(incentiveGroup.Priority, result.Priority);
            Assert.AreEqual(incentiveGroup.StartDateTime, result.StartDateTime);
            Assert.AreEqual(incentiveGroup.EndDateTime, result.EndDateTime);
        }

        [Test]
        public void should_return_database_incentive_group_model_from_convert_api_incentive_group_model_to_database_incentive_group_model()
        {
            var incentiveGroupModel = new IncentiveGroupModel
            {
                IncentiveGroupId = 101,
                LocationId = 202,
                Name = "IncentiveGroup One",
                Priority = 1,
                StartDateTime = new DateTime(2016, 6, 1),
                EndDateTime = null
            };

            var result = IncentiveGroupModelConverter.ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(incentiveGroupModel);

            Assert.AreEqual(incentiveGroupModel.IncentiveGroupId, result.IncentiveGroupId);
            Assert.AreEqual(incentiveGroupModel.LocationId, result.LocationId);
            Assert.AreEqual(incentiveGroupModel.Name, result.Name);
            Assert.AreEqual(incentiveGroupModel.Priority, result.Priority);
            Assert.AreEqual(incentiveGroupModel.StartDateTime, result.StartDateTime);
            Assert.AreEqual(incentiveGroupModel.EndDateTime, result.EndDateTime);
        }

        [Test]
        public void should_return_updated_database_incentive_group_model_from_convert_api_incentive_group_model_to_database_incentive_group_model()
        {
            var incentiveGroupModel = new IncentiveGroupModel
            {
                IncentiveGroupId = 101,
                LocationId = 202,
                Name = "IncentiveGroup One",
                Priority = 1,
                StartDateTime = new DateTime(2016, 6, 1),
                EndDateTime = null
            };
            var incentiveGroup = new Data.IncentiveGroup
            {
                IncentiveGroupId = 444,
                LocationId = 555,
                Name = "zzzzzzzzz",
                Priority = 2,
                StartDateTime = null,
                EndDateTime = null
            };

            var result = IncentiveGroupModelConverter.ConvertApiIncentiveGroupModelToDatabaseIncentiveGroupModel(incentiveGroupModel, incentiveGroup);

            Assert.AreEqual(incentiveGroupModel.IncentiveGroupId, result.IncentiveGroupId);
            Assert.AreEqual(incentiveGroupModel.LocationId, result.LocationId);
            Assert.AreEqual(incentiveGroupModel.Name, result.Name);
            Assert.AreEqual(incentiveGroupModel.Priority, result.Priority);
            Assert.AreEqual(incentiveGroupModel.StartDateTime, result.StartDateTime);
            Assert.AreEqual(incentiveGroupModel.EndDateTime, result.EndDateTime);
        }
    }
}
