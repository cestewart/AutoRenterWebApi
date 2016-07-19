using System.IO;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Branding
{
    public class GetBranding : IGetBranding
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetBranding(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(string item)
        {
            var branding = _autoRenterDatabaseContext.Brandings.FirstOrDefault(i => i.Item == item);

            if (branding == null)
            {
                return new ResultModel
                {
                    Success = false,
                    Message = $"The branding '{item}' could not be found."
                };
            }
            return new ResultModel
            {
                Data = BrandingModelConverter.ConvertDatabaseBrandingModelToApiBrandingModel(branding),
                Success = true
            };
        }
    }
}