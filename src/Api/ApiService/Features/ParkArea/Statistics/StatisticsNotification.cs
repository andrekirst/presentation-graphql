using MediatR;

namespace ApiService.Features.ParkArea.Statistics;

public class StatisticsNotification : INotification
{
    public int ParkAreaId { get; set; }
    public DateTime When { get; set; }
    public int Free { get; set; }
}