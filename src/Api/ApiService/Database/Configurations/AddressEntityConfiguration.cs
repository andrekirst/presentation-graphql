using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder
            .HasOne(a => a.ParkArea)
            .WithOne(pa => pa.Address)
            .HasForeignKey<AddressEntity>(a => a.ParkAreaId);
    }
}