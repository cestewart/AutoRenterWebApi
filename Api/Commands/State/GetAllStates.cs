using System.Collections.Generic;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.State
{
    public class GetAllStates : IGetAllStates
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetAllStates(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute()
        {
            var locations = _autoRenterDatabaseContext.States;

            return new ResultModel
            {
                Data = locations?.Select(StateModelConverter.ConvertDatabaseStateModelToApiStateModel).ToList() ?? new List<StateModel>(),
                Success = true
            };
        }
    }
}