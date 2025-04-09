using ApiService.Database;
using ApiService.Models.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea.Statistics;

public class YearStatisticsNotificationHandler(IDbContextFactory<AppDbContext> dbContextFactory) : INotificationHandler<StatisticsNotification>
{
    public async Task Handle(StatisticsNotification notification, CancellationToken cancellationToken)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        

        await SaveValues(notification, dbContext, cancellationToken);
        await SaveAggregates(notification, dbContext, cancellationToken);
    }

    private static async Task SaveAggregates(StatisticsNotification notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        var year = notification.When.Year;
        var parkAreaId = notification.ParkAreaId;
        var free = notification.Free;
        
        var currentAggregateValues = await dbContext.YearStatisticsAggregates
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year)
            .Select(y => new
            {
                y.Minimum,
                y.Maximum,
                y.Average
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (currentAggregateValues is null)
        {
            var aggregate1 = new YearStatisticsAggregateEntity
            {
                ParkAreaId = parkAreaId,
                Year = year,
                Average = free,
                Minimum = free,
                Maximum = free
            };

            dbContext.YearStatisticsAggregates.Add(aggregate1);
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var minimumAndMaximum = await dbContext.YearStatisticsValues
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year)
            .Select(t => t.Value)
            .GroupBy(t => t)
            .Select(t => new
            {
                Maximum = t.Max(),
                Minimum = t.Min()
            })
            .SingleAsync(cancellationToken);

        var average = await dbContext.YearStatisticsValues
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year)
            .GroupBy(_ => 1)
            .Select(g => g.Sum(x => x.Value * x.Count) / (double)g.Sum(x => x.Count))
            .SingleAsync(cancellationToken);

        var aggregate2 = await dbContext.YearStatisticsAggregates
            .SingleAsync(y => y.ParkAreaId == parkAreaId &&
                              y.Year == year,
                cancellationToken);

        aggregate2.Minimum = minimumAndMaximum.Minimum;
        aggregate2.Maximum = minimumAndMaximum.Maximum;
        aggregate2.Average = average;

        dbContext.YearStatisticsAggregates.Update(aggregate2);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SaveValues(StatisticsNotification notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        var year = notification.When.Year;
        var parkAreaId = notification.ParkAreaId;
        var free = notification.Free;
        
        var values = await dbContext.YearStatisticsValues
            .FirstOrDefaultAsync(y => y.ParkAreaId == parkAreaId &&
                                      y.Year == year &&
                                      y.Value == free,
                cancellationToken);

        if (values is null)
        {
            values = new YearStatisticsValuesEntity
            {
                ParkAreaId = parkAreaId,
                Year = year,
                Count = 1,
                Value = notification.Free
            };

            dbContext.YearStatisticsValues.Add(values);
        }
        else
        {
            values.Count++;
            dbContext.YearStatisticsValues.Update(values);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}