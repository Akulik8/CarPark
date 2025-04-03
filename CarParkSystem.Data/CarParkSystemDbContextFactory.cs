using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarParkSystem.Data
{
    public class CarParkSystemDbContextFactory : IDesignTimeDbContextFactory<CarParkSystemDbContext>
    {
        public CarParkSystemDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CarParkSystemDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new CarParkSystemDbContext(builder.Options);
        }
    }
}