namespace Api.Converters
{
    public static class DataTypeConverter
    {
        public static int ToInt(string valueToConvert)
        {
            int intValue;
            if (int.TryParse(valueToConvert, out intValue))
            {
                return intValue;
            }
            return -1;
        }

        public static bool ToBool(string valueToConvert)
        {
            bool boolValue;
            if (bool.TryParse(valueToConvert, out boolValue))
            {
                return boolValue;
            }
            return false;
        }
    }
}