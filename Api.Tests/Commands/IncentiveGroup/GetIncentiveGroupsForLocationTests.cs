using System.Collections.Generic;
using Api.Commands.IncentiveGroup;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.IncentiveGroup
{
    [TestFixture]
    public class GetIncentiveGroupsForLocationTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

        private static FakeDbSet<Data.IncentiveGroup> GetMockedIncentiveGroupData()
        {
            var incentiveGroups = new List<Data.IncentiveGroup>
            {
                new Data.IncentiveGroup
                {
                    IncentiveGroupId = 101,
                    LocationId = 201,
                    Name = "IncentiveGroup One"
                },
                new Data.IncentiveGroup
                {
                    IncentiveGroupId = 102,
                    LocationId = 201,
                    Name = "IncentiveGroup Indy"
                },
                new Data.IncentiveGroup
                {
                    IncentiveGroupId = 103,
                    LocationId = 202,
                    Name = "IncentiveGroup Three"
                }
            };
            var incentiveGroupDbSet = new FakeDbSet<Data.IncentiveGroup>();
            incentiveGroupDbSet.SetData(incentiveGroups);
            return incentiveGroupDbSet;
        }

        [Test]
        public void should_return_incentive_groups_from_GetIncentiveGroupsForLocationFromDatabase()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.IncentiveGroups).Returns(GetMockedIncentiveGroupData().Object).Verifiable();

            var mockGetIncentiveGroupsForLocation = new Mock<GetIncentiveGroupsForLocation>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetIncentiveGroupsForLocation.Object.GetIncentiveGroupsForLocationFromDatabase(201);

            Assert.IsInstanceOf<List<Data.IncentiveGroup>>(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void should_return_list_of_incentive_group_models_from_Execute()
        {
            var incentiveGroups = new List<Data.IncentiveGroup>
            {
                new Data.IncentiveGroup { IncentiveGroupId = 101 },
                new Data.IncentiveGroup { IncentiveGroupId = 102 }
            };

            var mockGetIncentiveGroupsForLocation = new Mock<GetIncentiveGroupsForLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetIncentiveGroupsForLocation.Setup(i => i.GetIncentiveGroupsForLocationFromDatabase(It.IsAny<int>())).Returns(incentiveGroups).Verifiable();

            var result = mockGetIncentiveGroupsForLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, ((List<IncentiveGroupModel>)result.Data).Count);
            Assert.IsNull(result.Message);
            mockGetIncentiveGroupsForLocation.VerifyAll();
        }

        [Test]
        public void should_return_incentive_group_model_with_error_from_Execute()
        {
            var mockGetIncentiveGroupsForLocation = new Mock<GetIncentiveGroupsForLocation>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetIncentiveGroupsForLocation.Setup(i => i.GetIncentiveGroupsForLocationFromDatabase(It.IsAny<int>())).Returns((List<Data.IncentiveGroup>)null).Verifiable();

            var result = mockGetIncentiveGroupsForLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            mockGetIncentiveGroupsForLocation.VerifyAll();
        }
    }
}
