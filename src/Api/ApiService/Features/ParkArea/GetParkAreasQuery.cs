using ApiService.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea;

public class GetParkAreasQuery : IRequest<IQueryable<Models.Presentation.ParkArea>>
{   
}

public class GetParkAreasQueryHandler(AppDbContext dbContext) : IRequestHandler<GetParkAreasQuery, IQueryable<Models.Presentation.ParkArea>>
{
    public Task<IQueryable<Models.Presentation.ParkArea>> Handle(GetParkAreasQuery request, CancellationToken cancellationToken)
    {
        var result = dbContext.ParkAreas
            .AsNoTracking()
            .Select(pa => new Models.Presentation.ParkArea
            {
                Id = pa.Id,
                DisplayName = pa.DisplayName ?? string.Empty,
                Free = pa.Free,
                Total = pa.Total,
                LastUpdate = pa.LastUpdate
            });

        return Task.FromResult(result);
    }
}