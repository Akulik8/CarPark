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
    public class AccidentConfiguration : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.HasKey(a => a.AccidentID);
            builder.HasOne(a => a.Vehicle).WithMany(v => v.Accidents).HasForeignKey(a => a.VehicleID);
            builder.HasOne(a => a.Driver).WithMany(d => d.Accidents).HasForeignKey(a => a.DriverID);
        }
    }
}
