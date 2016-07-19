using System.IO;

namespace Api.Models
{
    public class BrandingModel
    {
        public string Item { get; set; }

        public string ContentType { get; set; }

        public Stream Image { get; set; }
    }
}