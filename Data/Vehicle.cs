using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        public string Vin { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int Miles { get; set; }

        public string Color { get; set; }

        [ForeignKey("IncentiveGroup")]
        public int IncentiveGroupId { get; set; }

        public IncentiveGroup IncentiveGroup { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        public bool RentToOwn { get; set; }

        [Required]
        public string Make { get; set; }

        public byte[] Image { get; set; }

        public byte[] Thumbnail { get; set; }
    }
}
