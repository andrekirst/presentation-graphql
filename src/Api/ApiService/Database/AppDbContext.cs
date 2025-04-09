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
        
        ConfigureStatisticsEntity<HourStatisticsAggregateEntity>(modelBuilder);
        ConfigureStatisticsEntity<HourStatisticsValuesEntity>(modelBuilder);
        ConfigureStatisticsEntity<DayStatisticsAggregateEntity>(modelBuilder);
        ConfigureStatisticsEntity<DayStatisticsValuesEntity>(modelBuilder);
        ConfigureStatisticsEntity<MonthStatisticsAggregateEntity>(modelBuilder);
        ConfigureStatisticsEntity<MonthStatisticsValuesEntity>(modelBuilder);
        ConfigureStatisticsEntity<YearStatisticsAggregateEntity>(modelBuilder);
        ConfigureStatisticsEntity<YearStatisticsValuesEntity>(modelBuilder);
    }

    private static void ConfigureStatisticsEntity<TEntity>(ModelBuilder modelBuilder)
        where TEntity : StatisticsBaseEntity
    {
        modelBuilder
            .Entity<TEntity>()
            .HasOne(t => t.ParkArea)
            .WithMany(GetNavigationName<TEntity>())
            .HasForeignKey(t => t.ParkAreaId);
    }

    private static readonly Type ParkAreaEntityType = typeof(ParkAreaEntity);
    private static readonly Type CollectionType = typeof(ICollection<>);
    
    private static string GetNavigationName<TEntity>()
    {
        var entityType = typeof(TEntity);
        var matchingProperty = ParkAreaEntityType
            .GetProperties()
            .FirstOrDefault(prop =>
                prop.PropertyType.IsGenericType &&
                CollectionType.IsAssignableFrom(prop.PropertyType.GetGenericTypeDefinition()) &&
                prop.PropertyType.GetGenericArguments()[0] == entityType);

        return matchingProperty?.Name ?? throw new InvalidOperationException($"No matching navigation property found in ParkAreaEntity for {entityType.Name}");
    }
}