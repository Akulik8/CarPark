using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParkSystem.Domain.Models;

namespace CarParkSystem.Data.EntityConfigurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.DocumentID);
            builder.HasOne(d => d.Vehicle).WithMany(v => v.Documents).HasForeignKey(d => d.VehicleID);
        }
    }

}
