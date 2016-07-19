using System;
using System.Web.Http;
using Api.Commands.Location;
using Api.Models;

namespace Api.Controllers
{
    public class LocationController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetLocation _getLocation;
        private readonly IGetAllLocations _getAllLocations;
        private readonly ISaveLocation _saveLocation;
        private readonly IDeleteLocation _deleteLocation;

        public LocationController(IErrorHandler errorHandler, IGetLocation getLocation, IGetAllLocations getAllLocations, ISaveLocation saveLocation, IDeleteLocation deleteLocation)
        {
            _errorHandler = errorHandler;
            _getLocation = getLocation;
            _getAllLocations = getAllLocations;
            _saveLocation = saveLocation;
            _deleteLocation = deleteLocation;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var result = _getAllLocations.Execute();
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _getLocation.Execute(id);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Post(LocationModel locationModel)
        {
            return Save(locationModel);
        }

        public IHttpActionResult Put(LocationModel locationModel)
        {
            return Save(locationModel);
        }

        [Route("api/location/save")]
        public virtual IHttpActionResult Save(LocationModel locationModel)
        {
            try
            {
                var result = _saveLocation.Execute(locationModel);
                return result.Success ? (IHttpActionResult) Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _deleteLocation.Execute(id);
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
