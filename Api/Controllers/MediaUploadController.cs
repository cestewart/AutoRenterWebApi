using System;
using System.Web;
using System.Web.Http;
using Api.Commands.Media;
using Api.Converters;
using Api.Validators;

namespace Api.Controllers
{
    public class MediaUploadController : ApiController
    {
        private readonly IErrorHandler _errorHandler;
        private readonly ISaveMedia _saveMedia;
        private readonly IFileUploadValidator _fileUploadValidator;

        public MediaUploadController(IErrorHandler errorHandler, ISaveMedia saveMedia, IFileUploadValidator fileUploadValidator)
        {
            _errorHandler = errorHandler;
            _saveMedia = saveMedia;
            _fileUploadValidator = fileUploadValidator;
        }

        [Authorize]
        public virtual IHttpActionResult Post()
        {
            return Put(0);
        }

        [Authorize]
        public virtual IHttpActionResult Put(int id)
        {
            try
            {
                var file = GetHttpPostedFile();
                var validationResult = _fileUploadValidator.IsValid(file);
                if (!validationResult.Success) return BadRequest(validationResult.Message);
                var result = _saveMedia.Execute(MediaModelConverter.ConvertHttpPostedFileToMediaModel(file, id));
                return result.Success ? (IHttpActionResult)Ok(result) : BadRequest(result.Message);
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
