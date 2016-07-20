using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        [MaxLength(100)]
        public string Vin { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public int Year { get; set; }

        public int Miles { get; set; }

        [MaxLength(100)]
        public string Color { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        public bool RentToOwn { get; set; }

        [Required]
        [MaxLength(100)]
        public string Make { get; set; }

        [ForeignKey("Media")]
        public int MediaId { get; set; }

        public Media Media { get; set; }
    }
}
