using Api.Models;

namespace Api.Commands.Branding
{
    public interface IGetBranding
    {
        ResultModel Execute(string item);
    }
}
