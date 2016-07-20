using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Media
{
    public class GetMedia : IGetMedia
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetMedia(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public virtual ResultModel Execute(int mediaId)
        {
            var media = _autoRenterDatabaseContext.Medias.FirstOrDefault(i => i.MediaId == mediaId);

            return new ResultModel
            {
                Data = MediaModelConverter.ConvertDatabaseMediaModelToApiMediaModel(media),
                Success = media != null,
                Message = media == null ? "The media could not be found." : null
            };
        }
    }
}