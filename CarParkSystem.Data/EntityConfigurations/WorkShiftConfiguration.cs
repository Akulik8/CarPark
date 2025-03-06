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
    public class WorkShiftConfiguration : IEntityTypeConfiguration<WorkShift>
    {
        public void Configure(EntityTypeBuilder<WorkShift> builder)
        {
            builder.HasKey(ws => ws.ShiftID);
            builder.HasOne(ws => ws.Driver).WithMany(d => d.WorkShifts).HasForeignKey(ws => ws.DriverID);
            builder.HasOne(ws => ws.Vehicle).WithMany(v => v.WorkShifts).HasForeignKey(ws => ws.VehicleID);
        }
    }
}
