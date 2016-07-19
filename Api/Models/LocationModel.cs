namespace Api.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string SiteId { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }

        public string State { get; set; }

        public string StateAbbreviation { get; set; }

        public int VehicleCount { get; set; }
    }
}