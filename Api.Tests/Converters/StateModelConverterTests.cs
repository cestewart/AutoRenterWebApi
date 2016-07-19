using Api.Converters;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class StateModelConverterTests
    {
        [Test]
        public void should_return_state_model_from_convert_database_state_model_to_api_state_model()
        {
            var state = new Data.State
            {
                StateId = 101,
                Name = "Indiana",
                Abbreviation = "IN"
            };

            var result = StateModelConverter.ConvertDatabaseStateModelToApiStateModel(state);

            Assert.AreEqual(state.StateId, result.StateId);
            Assert.AreEqual(state.Name, result.Name);
            Assert.AreEqual(state.Abbreviation, result.Abbreviation);
        }
    }
}
