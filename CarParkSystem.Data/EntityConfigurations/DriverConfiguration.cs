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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.DriverID);
            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.LicenseNumber).HasMaxLength(20);
            builder.Property(d => d.LicenseCategory).HasMaxLength(10);
            builder.Property(d => d.PhoneNumber).HasMaxLength(20);
            builder.Property(d => d.Address).HasMaxLength(100);

            builder.HasMany(d => d.Trips).WithOne(t => t.Driver).HasForeignKey(t => t.DriverID);
            builder.HasMany(d => d.Accidents).WithOne(a => a.Driver).HasForeignKey(a => a.DriverID);
            builder.HasMany(d => d.Violations).WithOne(v => v.Driver).HasForeignKey(v => v.DriverID);
            builder.HasMany(d => d.WorkShifts).WithOne(ws => ws.Driver).HasForeignKey(ws => ws.DriverID);
            //builder.HasMany(d => d.Assignments).WithOne(va => va.Driver).HasForeignKey(va => va.DriverID);
        }
    }
}
