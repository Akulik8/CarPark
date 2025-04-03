using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarParkSystem.Data.EntityConfigurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(b => b.BidID);

            builder.Property(b => b.Cargo).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Weight).IsRequired();
            builder.Property(b => b.Volume).IsRequired();
            builder.Property(b => b.From).IsRequired();
            builder.Property(b => b.To).IsRequired();
            builder.Property(b => b.Note).HasMaxLength(500);
            builder.Property(b => b.Status).IsRequired().HasMaxLength(50);

            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bids)
                   .HasForeignKey(b => b.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Subdivision)
                   .WithMany(s => s.Bids)
                   .HasForeignKey(b => b.SubdivisionID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}