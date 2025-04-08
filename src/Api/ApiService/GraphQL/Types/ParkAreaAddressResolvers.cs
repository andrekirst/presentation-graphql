using ApiService.Features.ParkArea;
using ApiService.Models.Presentation;
using MediatR;

namespace ApiService.GraphQL.Types;

public class ParkAreaAddressResolvers
{
    public static Task<Address?> GetAddressAsync(
        [Parent] ParkArea parkArea,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetParkAreaAddressQuery(parkArea), cancellationToken);
}