using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.Database;

public class ParkplatzDbContext(DbContextOptions<ParkplatzDbContext> options) : DbContext(options)
{
    public DbSet<ParkAreaEntity> ParkAreas => Set<ParkAreaEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ParkAreaEntity>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }
}