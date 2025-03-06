using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Data.EntityConfigurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.VehicleID);
            builder.Property(v => v.LicensePlate).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Make).IsRequired().HasMaxLength(50);
            builder.Property(v => v.Model).IsRequired().HasMaxLength(50);
            builder.Property(v => v.VIN).HasMaxLength(50);
            builder.Property(v => v.FuelType).HasMaxLength(30);
            builder.Property(v => v.Status).HasMaxLength(30);

            builder.HasMany(v => v.Trips).WithOne(t => t.Vehicle).HasForeignKey(t => t.VehicleID);
            builder.HasMany(v => v.FuelRecords).WithOne(f => f.Vehicle).HasForeignKey(f => f.VehicleID);
            builder.HasMany(v => v.Maintenances).WithOne(m => m.Vehicle).HasForeignKey(m => m.VehicleID);
            builder.HasMany(v => v.Repairs).WithOne(r => r.Vehicle).HasForeignKey(r => r.VehicleID);
            builder.HasMany(v => v.Accidents).WithOne(a => a.Vehicle).HasForeignKey(a => a.VehicleID);
            builder.HasMany(v => v.Insurances).WithOne(i => i.Vehicle).HasForeignKey(i => i.VehicleID);
            builder.HasMany(v => v.Alerts).WithOne(a => a.Vehicle).HasForeignKey(a => a.VehicleID);
            builder.HasMany(v => v.Expenses).WithOne(e => e.Vehicle).HasForeignKey(e => e.VehicleID);
            builder.HasMany(v => v.Violations).WithOne(vl => vl.Vehicle).HasForeignKey(vl => vl.VehicleID);
            builder.HasMany(v => v.Documents).WithOne(d => d.Vehicle).HasForeignKey(d => d.VehicleID);
            builder.HasMany(v => v.WorkShifts).WithOne(ws => ws.Vehicle).HasForeignKey(ws => ws.VehicleID);
            //builder.HasMany(v => v.Assignments).WithOne(va => va.Vehicle).HasForeignKey(va => va.VehicleID);
        }
    }
}
