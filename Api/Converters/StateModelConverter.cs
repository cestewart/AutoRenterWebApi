using Api.Models;

namespace Api.Converters
{
    public static class StateModelConverter
    {
        public static StateModel ConvertDatabaseStateModelToApiStateModel(Data.State state)
        {
            if (state == null) return null;

            return new StateModel
            {
                StateId = state.StateId,
                Name = state.Name,
                Abbreviation = state.Abbreviation
            };
        }
    }
}