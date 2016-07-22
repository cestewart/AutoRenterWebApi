using System;
using System.Web;
using System.Web.Http;
using Api.Commands.Vehicle;
using Api.Converters;

namespace Api.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetVehicle _getVehicle;
        private readonly ISaveVehicle _saveVehicle;
        private readonly IDeleteVehicle _deleteVehicle;
        private readonly IConvertHttpRequestToVehicleModel _convertHttpRequestToVehicleModel;

        public VehicleController(IErrorHandler errorHandler, IGetVehicle getVehicle, ISaveVehicle saveVehicle, IDeleteVehicle deleteVehicle, IConvertHttpRequestToVehicleModel convertHttpRequestToVehicleModel)
        {
            _errorHandler = errorHandler;
            _getVehicle = getVehicle;
            _saveVehicle = saveVehicle;
            _deleteVehicle = deleteVehicle;
            _convertHttpRequestToVehicleModel = convertHttpRequestToVehicleModel;
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _getVehicle.Execute(id);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Post()
        {
            try
            {
                var result = _saveVehicle.Execute(_convertHttpRequestToVehicleModel.Execute(HttpContext.Current.Request));
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _deleteVehicle.Execute(id);
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
