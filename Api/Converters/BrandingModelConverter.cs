using Api.Models;

namespace Api.Converters
{
    public class BrandingModelConverter
    {
        public static BrandingModel ConvertDatabaseBrandingModelToApiBrandingModel(Data.Branding branding)
        {
            if (branding == null) return null;

            return new BrandingModel
            {
                Item = branding.Item,
                ContentType = branding.ContentType,
                Image = StreamConverter.ConvertByteArrayToStream(branding.Image)
            };
        }

        public static Data.Branding ConvertApiBrandingModelToDatabaseBrandingModel(BrandingModel brandingModel, Data.Branding branding)
        {
            if (brandingModel == null) return null;

            branding.Image = StreamConverter.ConvertStreamToByteArray(brandingModel.Image);
            branding.ContentType = brandingModel.ContentType;

            return branding;
        }
    }
}