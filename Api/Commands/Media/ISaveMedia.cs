using Api.Models;

namespace Api.Commands.Media
{
    public interface ISaveMedia
    {
        ResultModel Execute(MediaModel mediaModel);
    }
}
