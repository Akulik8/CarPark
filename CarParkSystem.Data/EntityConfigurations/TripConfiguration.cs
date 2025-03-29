using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Data.EntityConfigurations
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.TripID);
            
            builder.Property(t => t.EndTime).IsRequired(false); 
            builder.Property(t => t.MileageAtEnd).IsRequired(false); 
            builder.Property(t => t.FuelUsed).IsRequired(false); 

            builder.HasOne(t => t.Vehicle).WithMany(v => v.Trips).HasForeignKey(t => t.VehicleID);
            builder.HasOne(t => t.Driver).WithMany(d => d.Trips).HasForeignKey(t => t.DriverID);
            builder.HasMany(t => t.Routes).WithOne(r => r.Trip).HasForeignKey(r => r.TripID);
        }
    }
}
