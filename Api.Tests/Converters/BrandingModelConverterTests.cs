using Api.Converters;
using Api.Models;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class BrandingModelConverterTests
    {
        [Test]
        public void should_return_branding_model_from_convert_database_branding_model_to_api_branding_model()
        {
            var branding = new Data.Branding
            {
                Item = "Logo",
                Image = new byte [] { 0, 0, 1, 2, 3 }
            };

            var result = BrandingModelConverter.ConvertDatabaseBrandingModelToApiBrandingModel(branding);

            Assert.AreEqual(branding.Item, result.Item);
            Assert.AreEqual(branding.Image, StreamConverter.ConvertStreamToByteArray(result.Image));
        }

        [Test]
        public void should_return_updated_database_branding_model_from_convert_api_branding_model_to_database_branding_model()
        {
            var brandingModel = new BrandingModel
            {
                Item = "Logo",
                Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 0, 0, 1, 2, 3 })
            };

            var branding = new Data.Branding
            {
                Item = "ShouldNotChange",
                Image = new byte[] { 1, 2, 1, 2, 3 }
            };

            var result = BrandingModelConverter.ConvertApiBrandingModelToDatabaseBrandingModel(brandingModel, branding);

            Assert.AreEqual("ShouldNotChange", result.Item);
            Assert.AreEqual(brandingModel.Image, StreamConverter.ConvertByteArrayToStream(result.Image));
        }
    }
}
