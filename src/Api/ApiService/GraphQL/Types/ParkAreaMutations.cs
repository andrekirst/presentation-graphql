using ApiService.Features.ParkArea;
using MediatR;

namespace ApiService.GraphQL.Types;

public class ParkAreaMutations
{
    public Task<ParkAreaAddedPayload> AddParkAreaAsync(
        AddParkAreaPayload payload,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) =>
        mediator.Send(new AddParkAreaCommand(payload), cancellationToken);
}