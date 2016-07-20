using System.IO;

namespace Api.Models
{
    public class MediaModel
    {
        public int MediaId { get; set; }

        public Stream File { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}