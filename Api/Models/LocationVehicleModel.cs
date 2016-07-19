using System.IO;

namespace Api.Models
{
    public class LocationVehicleModel
    {
        public int VehicleId { get; set; }

        public string Vin { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int Miles { get; set; }

        public string Color { get; set; }

        public string Location { get; set; }

        public int LocationId { get; set; }

        public Stream Thumbnail { get; set; }
    }
}