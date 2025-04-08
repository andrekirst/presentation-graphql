using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class ServiceTimeEntityConfiguration : IEntityTypeConfiguration<ServiceTimeEntity>
{
    public void Configure(EntityTypeBuilder<ServiceTimeEntity> builder)
    {
        builder
            .HasOne(st => st.ParkArea)
            .WithOne(pa => pa.ServiceTime)
            .HasForeignKey<ServiceTimeEntity>(st => st.ParkAreaId);
    }
}