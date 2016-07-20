using System;
using System.Data.Entity;

namespace Data
{
    public interface IAutoRenterDatabaseContext : IDisposable
    {
        DbSet<Location> Locations { get; set; }
        DbSet<State> States { get; set; }
        DbSet<IncentiveGroup> IncentiveGroups { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Media> Medias { get; set; }

        int SaveChanges();
    }
}
