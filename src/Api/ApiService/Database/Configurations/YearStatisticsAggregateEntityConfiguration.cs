using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class YearStatisticsAggregateEntityConfiguration : IEntityTypeConfiguration<YearStatisticsAggregateEntity>
{
    public void Configure(EntityTypeBuilder<YearStatisticsAggregateEntity> builder)
    {
        builder
            .HasOne(sbe => sbe.ParkArea)
            .WithMany(pa => pa.YearStatisticsAggregates)
            .HasForeignKey(sbe => sbe.ParkAreaId);
    }
}