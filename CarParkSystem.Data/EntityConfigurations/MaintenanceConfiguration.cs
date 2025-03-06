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
    public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasKey(m => m.MaintenanceID);
            builder.HasOne(m => m.Vehicle).WithMany(v => v.Maintenances).HasForeignKey(m => m.VehicleID);
        }
    }
}
