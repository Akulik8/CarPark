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
    public class ViolationConfiguration : IEntityTypeConfiguration<Violation>
    {
        public void Configure(EntityTypeBuilder<Violation> builder)
        {
            builder.HasKey(v => v.ViolationID);
            builder.HasOne(v => v.Driver).WithMany(d => d.Violations).HasForeignKey(v => v.DriverID);
            builder.HasOne(v => v.Vehicle).WithMany(vc => vc.Violations).HasForeignKey(v => v.VehicleID);
        }
    }
}
