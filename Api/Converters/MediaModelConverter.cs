using System.Web;
using Api.Models;

namespace Api.Converters
{
    public static class MediaModelConverter
    {
        public static MediaModel ConvertDatabaseMediaModelToApiMediaModel(Data.Media media)
        {
            if (media == null) return null;

            return new MediaModel
            {
                MediaId = media.MediaId,
                ContentType = media.ContentType,
                FileName = media.FileName,
                File = StreamConverter.ConvertByteArrayToStream(media.File)
            };
        }

        public static Data.Media ConvertApiMediaModelToDatabaseMediaModel(MediaModel media)
        {
            return media == null ? null : ConvertApiMediaModelToDatabaseMediaModel(media, new Data.Media());
        }

        public static Data.Media ConvertApiMediaModelToDatabaseMediaModel(MediaModel media, Data.Media databaseMedia)
        {
            if (media == null) return null;

            databaseMedia.MediaId = media.MediaId;
            databaseMedia.ContentType = media.ContentType;
            databaseMedia.FileName = media.FileName;
            databaseMedia.File = StreamConverter.ConvertStreamToByteArray(media.File);

            return databaseMedia;
        }

        public static MediaModel ConvertHttpPostedFileToMediaModel(HttpPostedFile file)
        {
            return ConvertHttpPostedFileToMediaModel(file, 0);
        }

        public static MediaModel ConvertHttpPostedFileToMediaModel(HttpPostedFile file, int mediaId)
        {
            return new MediaModel
            {
                MediaId = mediaId,
                ContentType = file.ContentType,
                FileName = file.FileName,
                File = file.InputStream
            };
        }
    }
}