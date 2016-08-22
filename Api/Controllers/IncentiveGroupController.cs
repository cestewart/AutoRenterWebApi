using System;
using System.Web.Http;
using Api.Authentication;
using Api.Commands.IncentiveGroup;
using Api.Models;

namespace Api.Controllers
{
    public class IncentiveGroupController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetIncentiveGroup _getIncentiveGroup;
        private readonly IGetIncentiveGroupsForLocation _getIncentiveGroupsForLocation;
        private readonly ISaveIncentiveGroup _saveIncentiveGroup;

        public IncentiveGroupController(IErrorHandler errorHandler, IGetIncentiveGroup getIncentiveGroup, IGetIncentiveGroupsForLocation getIncentiveGroupsForLocation, ISaveIncentiveGroup saveIncentiveGroup)
        {
            _errorHandler = errorHandler;
            _getIncentiveGroup = getIncentiveGroup;
            _getIncentiveGroupsForLocation = getIncentiveGroupsForLocation;
            _saveIncentiveGroup = saveIncentiveGroup;
        }


        [CustomAuthorize]
        [Route("api/location/{locationId}/incentivegroup")]
        public IHttpActionResult GetIncentiveGroupsForLocation(int locationId)
        {
            try
            {
                var result = _getIncentiveGroupsForLocation.Execute(locationId);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [CustomAuthorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _getIncentiveGroup.Execute(id);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [CustomAuthorize]
        public IHttpActionResult Post(IncentiveGroupModel incentiveGroupModel)
        {
            try
            {
                var result = _saveIncentiveGroup.Execute(incentiveGroupModel);
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
