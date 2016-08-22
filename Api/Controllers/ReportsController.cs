using System;
using System.Web.Http;
using Api.Authentication;
using Api.Commands.Reports;

namespace Api.Controllers
{
    public class ReportsController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetActiveRentToOwn _getActiveRentToOwn;

        public ReportsController(IErrorHandler errorHandler, IGetActiveRentToOwn getActiveRentToOwn)
        {
            _errorHandler = errorHandler;
            _getActiveRentToOwn = getActiveRentToOwn;
        }

        [CustomAuthorize]
        [Route("api/reports/activeRentToOwn")]
        [HttpGet]
        public virtual IHttpActionResult ActiveRentToOwn()
        {
            try
            {
                var result = _getActiveRentToOwn.Execute();
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
