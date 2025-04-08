using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarParkSystem.Data.EntityConfigurations
{
    public class SubdivisionConfiguration : IEntityTypeConfiguration<Subdivision>
    {
        public void Configure(EntityTypeBuilder<Subdivision> builder)
        {
            builder.HasKey(s => s.SubdivisionID);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Address).IsRequired().HasMaxLength(500);
            builder.Property(s => s.PhoneNumber).HasMaxLength(20);
            builder.Property(s => s.Status).IsRequired().HasMaxLength(50);

            builder.HasMany(s => s.Bids)
                   .WithOne(b => b.Subdivision)
                   .HasForeignKey(b => b.SubdivisionID);
        }
    }
}