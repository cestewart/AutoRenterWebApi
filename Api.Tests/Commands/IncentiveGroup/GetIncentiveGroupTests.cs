using System.Collections.Generic;
using Api.Commands.IncentiveGroup;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.IncentiveGroup
{
    [TestFixture]
    public class GetIncentiveGroupTests
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
                    Name = "IncentiveGroup One"
                },
                new Data.IncentiveGroup
                {
                    IncentiveGroupId = 102,
                    Name = "IncentiveGroup Indy"
                }
            };
            var incentiveGroupDbSet = new FakeDbSet<Data.IncentiveGroup>();
            incentiveGroupDbSet.SetData(incentiveGroups);
            return incentiveGroupDbSet;
        }

        [Test]
        public void should_return_incentive_group_from_GetIncentiveGroupFromDatabase()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.IncentiveGroups).Returns(GetMockedIncentiveGroupData().Object).Verifiable();

            var mockGetIncentiveGroup = new Mock<GetIncentiveGroup>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetIncentiveGroup.Object.GetIncentiveGroupFromDatabase(101);

            Assert.IsInstanceOf<Data.IncentiveGroup>(result);
        }

        [Test]
        public void should_return_incentive_group_model_from_Execute()
        {
            var mockGetIncentiveGroup = new Mock<GetIncentiveGroup>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetIncentiveGroup.Setup(i => i.GetIncentiveGroupFromDatabase(It.IsAny<int>())).Returns(new Data.IncentiveGroup { Name = "Location Three" }).Verifiable();

            var result = mockGetIncentiveGroup.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Location Three", ((IncentiveGroupModel)result.Data).Name);
            Assert.IsNull(result.Message);
            mockGetIncentiveGroup.VerifyAll();
        }

        [Test]
        public void should_return_incentive_group_model_with_error_from_Execute()
        {
            var mockGetLocation = new Mock<GetIncentiveGroup>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockGetLocation.Setup(i => i.GetIncentiveGroupFromDatabase(It.IsAny<int>())).Returns((Data.IncentiveGroup)null).Verifiable();

            var result = mockGetLocation.Object.Execute(101);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The incentive group could not be found.", result.Message);
            mockGetLocation.VerifyAll();
        }
    }
}
