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
        private readonly IAutoRenterApiConfiguration _autoRenterApiConfiguration;

        public LoginController(IErrorHandler errorHandler, IGetUserByUsernameAndPassword getUserByUsernameAndPassword, IAutoRenterApiConfiguration autoRenterApiConfiguration)
        {
            _errorHandler = errorHandler;
            _getUserByUsernameAndPassword = getUserByUsernameAndPassword;
            _autoRenterApiConfiguration = autoRenterApiConfiguration;
        }

        [AllowAnonymous]
        public IHttpActionResult Post(LoginModel loginModel)
        {
            try
            {
                var result = _getUserByUsernameAndPassword.Execute(loginModel);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_autoRenterApiConfiguration.IsAvailable);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

    }
}
