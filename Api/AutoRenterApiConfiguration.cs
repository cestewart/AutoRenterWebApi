﻿using Api.Converters;

namespace Api
{
    public class AutoRenterApiConfiguration : IAutoRenterApiConfiguration
    {
        public string TestEmail => System.Configuration.ConfigurationManager.AppSettings.Get("TestEmail");

        public int MaximumFileSizeInKb => DataTypeConverter.ToInt(System.Configuration.ConfigurationManager.AppSettings.Get("MaximumFileSizeInKb"));

        public string AcceptedFileTypes => System.Configuration.ConfigurationManager.AppSettings.Get("AcceptedFileTypes");
    }
}