using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class ParkingSlotHistoryEntityConfiguration : IEntityTypeConfiguration<ParkingSlotHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ParkingSlotHistoryEntity> builder)
    {
        builder
            .HasOne(psh => psh.ParkArea)
            .WithMany(pa => pa.ParkingSlotHistories)
            .HasForeignKey(psh => psh.ParkAreaId);
    }
}