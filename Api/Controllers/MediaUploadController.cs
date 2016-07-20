using System;
using System.Web;
using System.Web.Http;
using Api.Commands.Media;
using Api.Models;
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

        public IHttpActionResult Post()
        {
            return Save(0);
        }

        public IHttpActionResult Put(int id)
        {
            return Save(id);
        }

        [Route("api/mediaupload/save")]
        public virtual IHttpActionResult Save(int id)
        {
            try
            {
                var file = GetHttpPostedFile();
                var validationResult = _fileUploadValidator.IsValid(file);
                if (!validationResult.Success) return BadRequest(validationResult.Message);
                var result = _saveMedia.Execute(CreateMediaModel(id, file));
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

        public virtual MediaModel CreateMediaModel(int id, HttpPostedFile file)
        {
            return new MediaModel
            {
                MediaId = id,
                ContentType = file.ContentType,
                FileName = file.FileName,
                File = file.InputStream
            };
        }
    }
}
