using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class OperatorEntityConfiguration : IEntityTypeConfiguration<OperatorEntity>
{
    public void Configure(EntityTypeBuilder<OperatorEntity> builder)
    {
        builder
            .HasMany(o => o.ParkAreas)
            .WithOne(pa => pa.Operator)
            .HasForeignKey(pa => pa.OperatorId);
    }
}