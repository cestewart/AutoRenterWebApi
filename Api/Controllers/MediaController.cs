using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Api.Commands.Media;
using Api.Converters;
using Api.Models;
using Api.Properties;

namespace Api.Controllers
{
    public class MediaController : Controller
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IGetMedia _getMedia;

        public virtual int Resize => DataTypeConverter.ToInt(Request.QueryString.Get("resize"));

        public virtual bool Download => DataTypeConverter.ToBool(Request.QueryString.Get("download"));

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
                return result.Success ? GetFileStream(result) : GetDefaultImageFileStream();
            }
            catch (Exception exception)
            {
                _errorHandler.LogError(exception);
                return new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes(string.Empty)), "image/gif");
            }
        }

        public virtual FileResult GetFileStream(ResultModel result)
        {
            var media = (MediaModel) result.Data;
            if (Resize > 0)
            {
                return new FileStreamResult(ResizeImage(media.File), media.ContentType);
            }
            if (Download)
            {
                return new FileStreamResult(media.File, media.ContentType) { FileDownloadName = media.FileName };
            }
            return new FileStreamResult(media.File, media.ContentType);
        }

        public virtual Stream ResizeImage(Stream input)
        {
            var originalBitmap = new Bitmap(input);
            var height = (int)Math.Round(originalBitmap.Height*(Resize / (decimal)originalBitmap.Width));
            var stream = new MemoryStream();

            using (var image = Image.FromStream(input))
            using (var bitmap = new Bitmap(Resize, height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Rectangle(0, 0, Resize, height));
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return stream;
            }
        }

        public virtual FileResult GetDefaultImageFileStream()
        {
            var stream = new MemoryStream();
            Resources.ImageMissing.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return new FileStreamResult(stream, "image/png");
        }
    }
}