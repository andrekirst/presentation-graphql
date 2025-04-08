using ApiService.Features.ParkArea;
using ApiService.Models.Presentation;
using MediatR;

namespace ApiService.GraphQL.Types;

public class ParkAreaPublicTransportationInformationResolvers
{
    public async Task<PublicTransportInformation> GetNearbyPublicTransportInformationAsync(
        [Parent] ParkArea parkArea,
        int? limit,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPublicTransportationQuery(parkArea)
        {
            Limit = limit
        }, cancellationToken);
    }
}