using System.Security.Cryptography;
using System.Text;

namespace Api.Converters
{
    public static class HashConverter
    {
        private const string HexFormat = "X2";

        public static string ToHash(string input)
        {
            return string.IsNullOrEmpty(input) ? null : ConvertHashToHexadecimal(CalculateHashFromString(input));
        }

        public static string ConvertHashToHexadecimal(byte[] hash)
        {
            var stringBuilder = new StringBuilder();
            foreach (var byteValue in hash)
            {
                stringBuilder.Append(byteValue.ToString(HexFormat));
            }
            return stringBuilder.ToString();
        }

        public static byte[] CalculateHashFromString(string input)
        {
            return MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
        }
    }
}
