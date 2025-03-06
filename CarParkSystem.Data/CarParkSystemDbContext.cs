using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Principal;

namespace CarParkSystem.Data
{
    public class CarParkSystemDbContext : DbContext
    {
      
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<FuelRecord> FuelRecords => Set<FuelRecord>();
        public DbSet<Maintenance> MaintenanceRecords => Set<Maintenance>();
        public DbSet<Repair> Repairs => Set<Repair>();
        public DbSet<Accident> Accidents => Set<Accident>();
        public DbSet<Insurance> Insurances => Set<Insurance>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Alert> Alerts => Set<Alert>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Violation> Violations => Set<Violation>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<WorkShift> WorkShifts => Set<WorkShift>();
        //public DbSet<VehicleAssignment> VehicleAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port = 5555; Database = car_park_db; Username = postgres; Password = 2616");
        }
    }
}
