using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class ParkingStateEntityConfiguration : IEntityTypeConfiguration<ParkingStateEntity>
{
    public void Configure(EntityTypeBuilder<ParkingStateEntity> builder)
    {
        builder
            .HasMany(ps => ps.ParkAreas)
            .WithOne(pa => pa.ParkingState)
            .HasForeignKey(pa => pa.ParkingStateId);
    }
}