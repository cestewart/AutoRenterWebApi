using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }

        [MaxLength(2)]
        [Required]
        [Index(IsUnique = true)]
        public string Abbreviation { get; set; }

        [MaxLength(100)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
