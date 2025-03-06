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
    public class FuelRecordConfiguration : IEntityTypeConfiguration<FuelRecord>
    {
        public void Configure(EntityTypeBuilder<FuelRecord> builder)
        {
            builder.HasKey(f => f.FuelRecordID);
            builder.HasOne(f => f.Vehicle).WithMany(v => v.FuelRecords).HasForeignKey(f => f.VehicleID);
        }
    }
}
