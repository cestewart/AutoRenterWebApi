using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Api.Commands.Media;
using Api.Converters;
using Api.Models;

namespace Api.Controllers
{
    public class MediaController : Controller
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetMedia _getMedia;

        public MediaController(IErrorHandler errorHandler, IGetMedia getMedia)
        {
            _errorHandler = errorHandler;
            _getMedia = getMedia;
        }

        public virtual FileResult Index(int id)
        {
            try
            {
                var result = _getMedia.Execute(id);
                return MediaConverter.ConvertMediaToFileStream((MediaModel)result.Data, DataTypeConverter.ToInt(Request.QueryString.Get("resize")), DataTypeConverter.ToBool(Request.QueryString.Get("download")));
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes(string.Empty)), "image/gif");
            }
        }
    }
}