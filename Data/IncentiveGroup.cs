using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class IncentiveGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncentiveGroupId { get; set; }

        [ForeignKey("Location")]
        [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        [Required]
        public int Priority { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}
