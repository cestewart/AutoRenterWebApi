using Api.Models;

namespace Api.Commands.Media
{
    public interface IGetMedia
    {
        ResultModel Execute(int mediaId);
    }
}
