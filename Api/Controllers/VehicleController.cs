using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Http;
using Api.Commands.Vehicle;
using Api.Converters;
using Api.Models;

namespace Api.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetVehicle _getVehicle;
        private readonly ISaveVehicle _saveVehicle;
        private readonly IDeleteVehicle _deleteVehicle;

        public VehicleController(IErrorHandler errorHandler, IGetVehicle getVehicle, ISaveVehicle saveVehicle, IDeleteVehicle deleteVehicle)
        {
            _errorHandler = errorHandler;
            _getVehicle = getVehicle;
            _saveVehicle = saveVehicle;
            _deleteVehicle = deleteVehicle;
        }

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

        public IHttpActionResult Post()
        {
            return Save(HttpContext.Current.Request);
        }

        public IHttpActionResult Put()
        {
            return Save(HttpContext.Current.Request);
        }

        [Route("api/vehicle/save")]
        public virtual IHttpActionResult Save(HttpRequest httpRequest)
        {
            try
            {
                var result = _saveVehicle.Execute(GetVehicleModel(httpRequest));
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public virtual VehicleModel GetVehicleModel(HttpRequest httpRequest)
        {

            var vehicleModel = GetVehicleModelData(httpRequest.Form);
//            vehicleModel.Image = httpRequest.Files["Image"]?.InputStream;
//            vehicleModel.Thumbnail = httpRequest.Files["Thumbnail"]?.InputStream;
            return vehicleModel;
        }

        public virtual VehicleModel GetVehicleModelData(NameValueCollection form)
        {
            return new VehicleModel
            {
                VehicleId = DataTypeConverter.ToInt(form["VehicleId"]),
                Vin = form["Vin"],
                Model = form["Model"],
                Year = DataTypeConverter.ToInt(form["Year"]),
                Miles = DataTypeConverter.ToInt(form["Miles"]),
                Color = form["Color"],
                LocationId = DataTypeConverter.ToInt(form["LocationId"]),
                RentToOwn = DataTypeConverter.ToBool(form["RentToOwn"]),
                Make = form["Make"],
            };
        }

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
