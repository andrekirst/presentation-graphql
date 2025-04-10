using ApiService.Database;
using ApiService.Models.Presentation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea.Statistics;

public class GetTodayStatisticsQuery : IRequest<TodayStatistic>
{
}

public class GetTodayStatisticsQueryHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetTodayStatisticsQuery, TodayStatistic>
{
    public async Task<TodayStatistic> Handle(GetTodayStatisticsQuery request, CancellationToken cancellationToken)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return new TodayStatistic
        {
            Hours = [
                new HourValue
                {
                    Minimum = 1,
                    Maximum = 3,
                    Average = 2.23
                }
            ] 
        };
    }
}