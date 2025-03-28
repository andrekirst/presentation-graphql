using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.Database;

public class ParkplatzDbContext(DbContextOptions<ParkplatzDbContext> options) : DbContext(options)
{
    public DbSet<ParkArea> ParkAreas => Set<ParkArea>();
    public DbSet<ParkingSlotsHistory> ParkingSlotsHistories => Set<ParkingSlotsHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParkplatzDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }
}