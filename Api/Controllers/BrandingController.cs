using System;
using System.Web;
using System.Web.Http;
using Api.Commands.Branding;
using Api.Converters;
using Api.Models;
using Api.Validators;

namespace Api.Controllers
{
    public class BrandingController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IFileUploadValidator _fileUploadValidator;
        private readonly IGetBranding _getBranding;
        private readonly ISaveBranding _saveBranding;

        public BrandingController(IErrorHandler errorHandler, IFileUploadValidator fileUploadValidator, IGetBranding getBranding, ISaveBranding saveBranding)
        {
            _errorHandler = errorHandler;
            _fileUploadValidator = fileUploadValidator;
            _getBranding = getBranding;
            _saveBranding = saveBranding;
        }

        [Route("api/branding/{item}/")]
        public IHttpActionResult Get(string item)
        {
            try
            {
                var result = _getBranding.Execute(item);
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        [Route("api/branding/{item}/")]
        public IHttpActionResult Post(string item)
        {
            try
            {
                var file = GetHttpPostedFile();
                var resultModel = _fileUploadValidator.IsValid(file);
                if (resultModel.Success)
                {
                    var brandingModel = new BrandingModel
                    {
                        Item = item,
                        ContentType = file.ContentType,
                        Image = file.InputStream
                    };
                    resultModel = _saveBranding.Execute(brandingModel);
                }
                return Ok(resultModel);
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return BadRequest(exception.Message);
            }
        }

        public virtual HttpPostedFile GetHttpPostedFile()
        {
            return HttpContext.Current.Request.Files[0];
        }
    }
}
