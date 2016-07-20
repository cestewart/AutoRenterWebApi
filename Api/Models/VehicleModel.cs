using System.IO;

namespace Api.Models
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }

        public string Vin { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int Miles { get; set; }

        public string Color { get; set; }

        public int LocationId { get; set; }

        public LocationModel Location { get; set; }

        public bool RentToOwn { get; set; }

        public string Make { get; set; }

        public int MediaId { get; set; }
    }
}