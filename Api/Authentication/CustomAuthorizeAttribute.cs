using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Api.Authentication
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly ITokenManager _tokenManager;

        public CustomAuthorizeAttribute(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAuthorized(actionContext))
            {
                base.OnAuthorization(actionContext);
                return;
            }
            HandleUnauthorizedRequest(actionContext);                
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return SkipAuthorization(actionContext) || AuthorizeRequest(actionContext);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                   actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            var bearerToken = GetBearerTokenFromHeader(actionContext);
            return !string.IsNullOrEmpty(bearerToken) && _tokenManager.IsTokenValid(bearerToken);
        }

        public string GetBearerTokenFromHeader(HttpActionContext actionContext)
        {
            try
            {
                var headerValues = actionContext.Request.Headers.GetValues("BearerToken");
                return headerValues.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}