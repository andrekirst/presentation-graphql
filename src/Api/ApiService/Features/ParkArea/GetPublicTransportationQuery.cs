using ApiService.Database;
using ApiService.Services;
using DvbCs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea;

public class GetPublicTransportationQuery(Models.Presentation.ParkArea parkArea) : IRequest<PublicTransportInformation>
{
    public Models.Presentation.ParkArea ParkArea { get; } = parkArea;

    public int? Limit { get; set; }
}

public class PublicTransportInformation
{
    public List<Departure> Departures { get; set; } = [];
}

public class Departure
{
    public string LineNumber { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime DepartureTimeAt { get; set; }
    public int DepartureTimeInMinutes { get; set; }
}

public class GetPublicTransportationQueryHandler(
    IDbContextFactory<AppDbContext> dbContextFactory,
    ParkAreaPublicStopsService parkAreaPublicStopsService,
    DepartureMonitorService departureMonitorService) : IRequestHandler<GetPublicTransportationQuery, PublicTransportInformation>
{
    public async Task<PublicTransportInformation> Handle(GetPublicTransportationQuery request, CancellationToken cancellationToken)
    {
        var stopId = parkAreaPublicStopsService.GetStopIds(request.ParkArea.Id);
        
        if (stopId is null)
        {
            return new PublicTransportInformation();
        }

        var options = new GetDepartureMonitorForStopIdOptions
        {
            Limit = request.Limit
        };
        
        var departureMonitor = await departureMonitorService.GetDepartureMonitorForStopIdAsync(stopId,options, cancellationToken);

        return new PublicTransportInformation
        {
            Departures = departureMonitor?.Departures
                .Select(d => new Departure
                {
                    Type = d.Mot,
                    DepartureTimeAt = d.RealTime.ToLocalTime(),
                    DepartureTimeInMinutes = (int)(d.RealTime.ToLocalTime() - DateTime.Now).TotalMinutes,
                    LineNumber = d.LineName ?? string.Empty,
                    Direction = d.Direction ?? string.Empty
                })
                .ToList() ?? []
        };
    }
}