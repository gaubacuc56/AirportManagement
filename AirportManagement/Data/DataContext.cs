using AirportManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        #region DbSet
        public DbSet<Aircraft> tblAircraft { get; set;}
        public DbSet<Airport> tblAirport { get; set; }
        public DbSet<City> tblCity { get; set; }
        public DbSet<Employee> tblEmployee { get; set; }
        public DbSet<Flight> tblFlight { get; set; }
        public DbSet<Luggage> tblLuggage { get; set; }
        public DbSet<Passenger> tblPassenger { get; set; }
        public DbSet<Runway> tblRunway { get; set; }
        public DbSet<Country> tblCountry { get; set; }
        public DbSet<SystemRole> tblSystemRole { get; set; }
        #endregion
        public override int SaveChanges()
        {
            // Check and enforce the constraint for the Role property
            foreach (var entry in ChangeTracker.Entries<Employee>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity.empRole != UserRole.Admin && entry.Entity.empRole != UserRole.Employee)
                {
                    throw new InvalidOperationException("Invalid User Role");
                }
            }

            return base.SaveChanges();
        }

        #region CreateRelationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().Ignore(t => t.Identification);

            #region SetDbDefault
            modelBuilder.Entity<City>()
                .Property(c => c.cityId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Country>()
                .Property(c => c.countryId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Aircraft>()
                .Property(c => c.aircraftId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Airport>()
                .Property(c => c.airportId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Employee>()
                .Property(c => c.employeeId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Luggage>()
                .Property(c => c.luggageId )
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Runway>()
                .Property(c => c.runwayId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Employee>()
                .Property(c => c.employeeId)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Employee>()
                .ToTable(emp => emp.HasCheckConstraint("ck_emp_role", "empRole IN (0, 1)"));

            modelBuilder.Entity<SystemRole>()
                .ToTable(r => r.HasCheckConstraint("ck_role_name", "RoleName IN ('Admin', 'Employee')"));
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
