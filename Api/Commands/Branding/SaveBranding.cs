using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Branding
{
    public class SaveBranding : ISaveBranding
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveBranding(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(BrandingModel brandingModel)
        {
            var result = SaveBrandingRecord(brandingModel);
            return new ResultModel
            {
                Success = result,
                Message = result ? null : $"The branding item '{brandingModel.Item}' could not be found."
            };
        }

        public virtual bool SaveBrandingRecord(BrandingModel brandingModel)
        {
            var branding = _autoRenterDatabaseContext.Brandings.FirstOrDefault(i => i.Item == brandingModel.Item.ToLower());
            if (branding == null) return false;
            BrandingModelConverter.ConvertApiBrandingModelToDatabaseBrandingModel(brandingModel, branding);
            _autoRenterDatabaseContext.SaveChanges();
            return true;
        }
    }
}