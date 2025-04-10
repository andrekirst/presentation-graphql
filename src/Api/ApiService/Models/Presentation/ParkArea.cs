using ApiService.Features.ParkArea.Statistics;
using MediatR;

namespace ApiService.Models.Presentation;

public class ParkArea : Model
{
    public string DisplayName { get; set; } = null!;
    public DateTime? LastUpdate { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }
    
    public Address? Address { get; set; }

    public Statistics GetStatistics() => new Statistics();
}

public class Statistics
{
    public DayStatistic GetDay() => new DayStatistic();
}

public class DayStatistic
{
    public async Task<TodayStatistic> GetTodayAsync([Service] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetTodayStatisticsQuery(), cancellationToken);
    }
}

public class SpecificDayStatistic
{
    public List<HourValue> Hours { get; set; } = [];
}

public class TodayStatistic
{
    public List<HourValue> Hours { get; set; } = [];
}

public class HourValue
{
    public int Maximum { get; set; }
    public int Minimum { get; set; }
    public double Average { get; set; }
}
