using Api.Models;

namespace Api.Commands.Branding
{
    public interface ISaveBranding
    {
        ResultModel Execute(BrandingModel brandingModel);
    }
}
