using System;
using System.Collections.Generic;
using Api.Commands.State;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.State
{
    [TestFixture]
    public partial class GetAllStatesTests
    {
        private static FakeDbSet<Data.State> GetMockedStateData()
        {
            var states = new List<Data.State>
            {
                new Data.State
                {
                    StateId = 101,
                    Name = "Indiana",
                    Abbreviation = "IN"
                },
                new Data.State
                {
                    StateId = 102,
                    Name = "Texas",
                    Abbreviation = "TX"
                }
            };
            var stateDbSet = new FakeDbSet<Data.State>();
            stateDbSet.SetData(states);
            return stateDbSet;
        }

        [Test]
        public void should_return_list_of_states_from_execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> {CallBase = true};
            mockAutoRenterDatabaseContext.Setup(i => i.States).Returns(GetMockedStateData().Object).Verifiable();

            var mockGetAllStates = new Mock<GetAllStates>(mockAutoRenterDatabaseContext.Object) {CallBase = true};

            var result = mockGetAllStates.Object.Execute();

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, ((List<StateModel>) result.Data).Count);
            Assert.IsNull(result.Message);
            mockAutoRenterDatabaseContext.VerifyAll();
        }
    }
}
