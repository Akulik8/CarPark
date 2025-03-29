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
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.RouteID);
            builder.Property(r => r.StartPoint).IsRequired().HasMaxLength(100);
            builder.Property(r => r.EndPoint).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Distance).IsRequired();

            builder.HasOne(t => t.Trip).WithMany(r => r.Routes).HasForeignKey(t=> t.TripID);
        }
    }
}
