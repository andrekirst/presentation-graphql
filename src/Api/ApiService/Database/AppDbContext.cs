using ApiService.Models.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ParkAreaEntity> ParkAreas => Set<ParkAreaEntity>();
    public DbSet<ParkingSlotHistoryEntity> ParkingSlotHistories => Set<ParkingSlotHistoryEntity>();
    public DbSet<ParkingStateEntity> ParkingStates => Set<ParkingStateEntity>();
    public DbSet<OperatorEntity> Operators => Set<OperatorEntity>();
    public DbSet<RegionEntity> Regions => Set<RegionEntity>();
    public DbSet<AddressEntity> Addresses => Set<AddressEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}