using ApiService.Database;
using ApiService.Models.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea.Statistics;

public class HourStatisticsNotificationHandler(IDbContextFactory<AppDbContext> dbContextFactory) : INotificationHandler<StatisticsNotification>
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
        var month = notification.When.Month;
        var day = notification.When.Day;
        var hour = notification.When.Hour;
        var parkAreaId = notification.ParkAreaId;
        var free = notification.Free;
        
        var currentAggregateValues = await dbContext.HourStatisticsAggregates
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year &&
                        y.Month == month &&
                        y.Day == day &&
                        y.Hour == hour)
            .Select(y => new
            {
                y.Minimum,
                y.Maximum,
                y.Average
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (currentAggregateValues is null)
        {
            var aggregate1 = new HourStatisticsAggregateEntity
            {
                ParkAreaId = parkAreaId,
                Year = year,
                Month = month,
                Day = day,
                Hour = hour,
                Average = free,
                Minimum = free,
                Maximum = free
            };

            dbContext.HourStatisticsAggregates.Add(aggregate1);
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var minimum = await dbContext.HourStatisticsValues
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year &&
                        y.Month == month &&
                        y.Day == day &&
                        y.Hour == hour)
            .MinAsync(t => t.Value, cancellationToken);
        
        var maximum = await dbContext.HourStatisticsValues
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year &&
                        y.Month == month &&
                        y.Day == day &&
                        y.Hour == hour)
            .MaxAsync(t => t.Value, cancellationToken);

        var valuesForAggregation = await dbContext.HourStatisticsValues
            .Where(y => y.ParkAreaId == parkAreaId &&
                        y.Year == year &&
                        y.Month == month &&
                        y.Day == day &&
                        y.Hour == hour)
            .Select(t => new
            {
                t.Count,
                t.Value
            })
            .ToListAsync(cancellationToken);

        var summe = valuesForAggregation.Sum(t => t.Value * t.Count);
        var count = valuesForAggregation.Sum(t => t.Count);

        var average = summe / (double)count;

        var aggregate2 = await dbContext.HourStatisticsAggregates
            .SingleAsync(y => y.ParkAreaId == parkAreaId &&
                              y.Year == year &&
                              y.Month == month &&
                              y.Day == day &&
                              y.Hour == hour,
                cancellationToken);

        aggregate2.Minimum = minimum;
        aggregate2.Maximum = maximum;
        aggregate2.Average = average;

        dbContext.HourStatisticsAggregates.Update(aggregate2);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SaveValues(StatisticsNotification notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        var year = notification.When.Year;
        var month = notification.When.Month;
        var day = notification.When.Day;
        var hour = notification.When.Hour;
        var parkAreaId = notification.ParkAreaId;
        var free = notification.Free;
        
        var values = await dbContext.HourStatisticsValues
            .FirstOrDefaultAsync(y => y.ParkAreaId == parkAreaId &&
                                      y.Year == year &&
                                      y.Month == month &&
                                      y.Day == day &&
                                      y.Hour == hour &&
                                      y.Value == free,
                cancellationToken);

        if (values is null)
        {
            values = new HourStatisticsValuesEntity
            {
                ParkAreaId = parkAreaId,
                Year = year,
                Month = month,
                Day = day,
                Hour = hour,
                Count = 1,
                Value = notification.Free
            };

            dbContext.HourStatisticsValues.Add(values);
        }
        else
        {
            values.Count++;
            dbContext.HourStatisticsValues.Update(values);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}