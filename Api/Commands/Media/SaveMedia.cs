using System.Collections.Generic;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Media
{
    public class SaveMedia : ISaveMedia
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveMedia(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public virtual ResultModel Execute(MediaModel mediaModel)
        {
            return mediaModel.MediaId == 0 ? CreateMedia(mediaModel) : UpdateMedia(mediaModel);
        }

        public virtual ResultModel CreateMedia(MediaModel mediaModel)
        {
            var media = MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(mediaModel);
            _autoRenterDatabaseContext.Medias.Add(media);
            _autoRenterDatabaseContext.SaveChanges();
            mediaModel.MediaId = media.MediaId;
            mediaModel.File = null;
            return new ResultModel
            {
                Data = mediaModel,
                Success = true
            };
        }

        public virtual ResultModel UpdateMedia(MediaModel mediaModel)
        {
            var media = _autoRenterDatabaseContext.Medias.FirstOrDefault(i => i.MediaId == mediaModel.MediaId);
            if (media == null) return new ResultModel {Success = false, Messages = new List<string> {"Unable to find media to update."}};
            MediaModelConverter.ConvertApiMediaModelToDatabaseMediaModel(mediaModel, media);
            _autoRenterDatabaseContext.SaveChanges();
            mediaModel.File = null;
            return new ResultModel
            {
                Data = mediaModel,
                Success = true
            };
        }
    }
}