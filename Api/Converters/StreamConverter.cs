using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Api.Converters
{
    public static class StreamConverter
    {
        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static Stream ConvertByteArrayToStream(byte[] file)
        {
            return new MemoryStream(file.ToArray(), true);
        }
    }
}