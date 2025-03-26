using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Models.Database;
using ParkplatzDresden.ApiService.Models.Domain;

namespace ParkplatzDresden.ApiService.Database;

public class ParkplatzDbContext(DbContextOptions<ParkplatzDbContext> options) : DbContext(options)
{
    public DbSet<ParkAreaEntity> ParkAreas => Set<ParkAreaEntity>();
    public DbSet<ParkingSlotsHistoryEntity> ParkingSlotsHistories => Set<ParkingSlotsHistoryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ParkAreaEntity>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder
            .Entity<ParkAreaEntity>()
            .HasMany(pa => pa.ParkingSlotsHistory)
            .WithOne(psh => psh.ParkArea)
            .HasForeignKey(psh => psh.ParkAreaId);

        modelBuilder
            .Entity<ParkAreaEntity>()
            .HasOne(pa => pa.ParkingSlot)
            .WithOne(ps => ps.ParkArea)
            .HasForeignKey<ParkingSlotsEntity>(ps => ps.ParkAreaEntityId)
            .IsRequired();

        modelBuilder
            .Entity<ParkingSlotsEntity>()
            .ToTable("ParkingSlots");

        modelBuilder
            .Entity<ParkingSlotsHistoryEntity>()
            .ToTable("ParkingSlotsHistory");

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }
}