using System;
using System.Web.Http;
using Api.Commands.Login;
using Api.Models;

namespace Api.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetUserByUsernameAndPassword _getUserByUsernameAndPassword;

        public LoginController(IErrorHandler errorHandler, IGetUserByUsernameAndPassword getUserByUsernameAndPassword)
        {
            _errorHandler = errorHandler;
            _getUserByUsernameAndPassword = getUserByUsernameAndPassword;
        }

        [AllowAnonymous]
        public IHttpActionResult Post(LoginModel loginModel)
        {
            try
            {
                var result = _getUserByUsernameAndPassword.Execute(loginModel);
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
