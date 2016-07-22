using System;
using System.Web.Http;
using Api.Commands.State;

namespace Api.Controllers
{
    public class StateController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetAllStates _getAllStates;

        public StateController(IErrorHandler errorHandler, IGetAllStates getAllStates)
        {
            _errorHandler = errorHandler;
            _getAllStates = getAllStates;
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            try
            {
                var result = _getAllStates.Execute();
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }
    }
}
