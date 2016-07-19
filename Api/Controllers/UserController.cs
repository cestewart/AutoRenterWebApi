using System;
using System.Web.Http;
using Api.Commands.User;
using Api.Models;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetUser _getUser;
        private readonly ISearchForUsers _searchForUsers;
        private readonly ISaveUser _saveUser;
        private readonly IDeleteUser _deleteUser;

        public UserController(IErrorHandler errorHandler, IGetUser getUser, ISearchForUsers searchForUsers, ISaveUser saveUser, IDeleteUser deleteUser)
        {
            _errorHandler = errorHandler;
            _getUser = getUser;
            _searchForUsers = searchForUsers;
            _saveUser = saveUser;
            _deleteUser = deleteUser;
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _getUser.Execute(id);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Get(string searchTerm)
        {
            try
            {
                var result = _searchForUsers.Execute(searchTerm);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Post(UserModel userModel)
        {
            return Save(userModel);
        }

        public IHttpActionResult Put(UserModel userModel)
        {
            return Save(userModel);
        }

        [Route("api/user/save")]
        public virtual IHttpActionResult Save(UserModel userModel)
        {
            try
            {
                var result = _saveUser.Execute(userModel);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
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
                var result = _deleteUser.Execute(id);
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
