using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiService.Database.Configurations;

public class ParkAreaEntityConfiguration : IEntityTypeConfiguration<ParkAreaEntity>
{
    public void Configure(EntityTypeBuilder<ParkAreaEntity> builder)
    {
    }
}