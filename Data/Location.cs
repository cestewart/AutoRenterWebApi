using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(3)]
        [Required]
        public string SiteId { get; set; }

        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        [ForeignKey("State")]
        [Required]
        public int StateId { get; set; }

        public State State { get; set; }

        public List<IncentiveGroup> IncentiveGroups { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
