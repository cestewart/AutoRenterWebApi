using Api.Converters;

namespace Api
{
    public class AutoRenterApiConfiguration : IAutoRenterApiConfiguration
    {
        public int MaximumFileSizeInKb => DataTypeConverter.ToInt(System.Configuration.ConfigurationManager.AppSettings.Get("MaximumFileSizeInKb"));

        public string AcceptedFileTypes => System.Configuration.ConfigurationManager.AppSettings.Get("AcceptedFileTypes");

        public bool IsAvailable => DataTypeConverter.ToBool(System.Configuration.ConfigurationManager.AppSettings.Get("IsAvailable"));
    }
}