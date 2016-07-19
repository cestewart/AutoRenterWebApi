using System.Collections.Generic;
using Api.Commands.IncentiveGroup;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.IncentiveGroup
{
    [TestFixture]
    public class SaveIncentiveGroupTests
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
        public void execute_should_create_new_incentive_group_record_by_calling_create_incentive_group()
        {
            var mockSaveIncentiveGroup = new Mock<SaveIncentiveGroup>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveIncentiveGroup.Setup(i => i.CreateIncentiveGroup(It.IsAny<IncentiveGroupModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveIncentiveGroup.Object.Execute(new IncentiveGroupModel());

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveIncentiveGroup.Verify(i => i.CreateIncentiveGroup(It.IsAny<IncentiveGroupModel>()), Times.Once);
            mockSaveIncentiveGroup.Verify(i => i.UpdateIncentiveGroup(It.IsAny<IncentiveGroupModel>()), Times.Never);
        }

        [Test]
        public void execute_should_update_incentive_group_record_by_calling_update_incentive_group()
        {
            var mockSaveIncentiveGroup = new Mock<SaveIncentiveGroup>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveIncentiveGroup.Setup(i => i.UpdateIncentiveGroup(It.IsAny<IncentiveGroupModel>())).Returns(new ResultModel()).Verifiable();

            var result = mockSaveIncentiveGroup.Object.Execute(new IncentiveGroupModel { IncentiveGroupId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            mockSaveIncentiveGroup.Verify(i => i.CreateIncentiveGroup(It.IsAny<IncentiveGroupModel>()), Times.Never);
            mockSaveIncentiveGroup.Verify(i => i.UpdateIncentiveGroup(It.IsAny<IncentiveGroupModel>()), Times.Once);
        }

        [Test]
        public void create_incentive_group_should_create_IncentiveGroup_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.IncentiveGroups).Returns(GetMockedIncentiveGroupData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveIncentiveGroup = new Mock<SaveIncentiveGroup>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveIncentiveGroup.Object.CreateIncentiveGroup(new IncentiveGroupModel());

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void create_incentive_group_should_update_incentive_group_record()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.IncentiveGroups).Returns(GetMockedIncentiveGroupData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveIncentiveGroup = new Mock<SaveIncentiveGroup>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveIncentiveGroup.Object.UpdateIncentiveGroup(new IncentiveGroupModel { IncentiveGroupId = 101 });

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
