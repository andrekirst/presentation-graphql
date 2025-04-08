using ApiService.Features.ParkArea;
using ApiService.Models.Presentation;
using MediatR;

namespace ApiService.GraphQL.Types;

public class ParkAreaQueries
{
    public Task<ParkArea> GetParkAreaByIdAsync(
        int id,
        [Service] IMediator mediator,
        CancellationToken cancellationToken = default) =>
        mediator.Send(new GetParkAreaByIdQuery(id), cancellationToken);

    public Task<IQueryable<ParkArea>> GetParkAreas(
        [Service] IMediator mediator,
        CancellationToken cancellationToken = default) =>
        mediator.Send(new GetParkAreasQuery(), cancellationToken);
}