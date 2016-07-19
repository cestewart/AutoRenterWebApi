using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Api.Commands.Branding;
using Api.Models;

namespace Api.Controllers
{
    public class ImageController : Controller
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetBranding _getBranding;

        public ImageController(IErrorHandler errorHandler, IGetBranding getBranding)
        {
            _errorHandler = errorHandler;
            _getBranding = getBranding;
        }

        public FileResult Index(string item)
        {
            try
            {
                var result = _getBranding.Execute(item);
                if (!result.Success)
                {
                    result = _getBranding.Execute("default");
                }

                var branding = (BrandingModel)result.Data;
                return new FileStreamResult(branding.Image, branding.ContentType);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                var errorMessage = Encoding.UTF8.GetBytes("An error occured.  The image could not be found.");
                return new FileStreamResult(new MemoryStream(errorMessage), "text/HTML");
            }
        }
    }
}