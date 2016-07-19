using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Branding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandingId { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Item { get; set; }

        [Required]
        public string ContentType { get; set; }

        public byte[] Image { get; set; }
    }
}
