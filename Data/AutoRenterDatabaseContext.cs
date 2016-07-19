using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Migrations;

namespace Data
{
    public class AutoRenterDatabaseContext : DbContext, IAutoRenterDatabaseContext
    {
        public AutoRenterDatabaseContext()
            : base("name=AutoRenterDatabaseContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.CommandTimeout = 300;
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<IncentiveGroup> IncentiveGroups { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Branding> Brandings { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
