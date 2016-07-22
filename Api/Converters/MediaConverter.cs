using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Api.Models;
using Api.Properties;

namespace Api.Converters
{
    public static class MediaConverter
    {
        public static FileResult ConvertMediaToFileStream(MediaModel mediaModel, int resize, bool download)
        {
            if (mediaModel == null) return GetDefaultImageFileStream();
            if (resize > 0)
            {
                return new FileStreamResult(ResizeImage(mediaModel.File, resize), mediaModel.ContentType);
            }
            return download
                ? new FileStreamResult(mediaModel.File, mediaModel.ContentType) {FileDownloadName = mediaModel.FileName}
                : new FileStreamResult(mediaModel.File, mediaModel.ContentType);
        }

        public static Stream ResizeImage(Stream input, int resize)
        {
            var originalBitmap = new Bitmap(input);
            var height = (int)Math.Round(originalBitmap.Height * (resize / (decimal)originalBitmap.Width));
            var stream = new MemoryStream();

            using (var image = Image.FromStream(input))
            using (var bitmap = new Bitmap(resize, height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Rectangle(0, 0, resize, height));
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return stream;
            }
        }

        public static FileResult GetDefaultImageFileStream()
        {
            var stream = new MemoryStream();
            Resources.ImageMissing.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return new FileStreamResult(stream, "image/png");
        }
    }
}