using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class RegionEntityConfiguration : IEntityTypeConfiguration<RegionEntity>
{
    public void Configure(EntityTypeBuilder<RegionEntity> builder)
    {
        builder
            .HasMany(r => r.ParkAreas)
            .WithOne(pa => pa.Region)
            .HasForeignKey(pa => pa.RegionId);
    }
}