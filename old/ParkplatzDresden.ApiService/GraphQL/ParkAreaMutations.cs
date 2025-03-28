using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.GraphQL;

[ExtendObjectType(KnownTypeNames.Mutation)]
public class ParkAreaMutations
{
    public async Task<ParkAreaUpdatedPayload> UpdateParkArea(
        ParkplatzDbContext dbContext,
        ITopicEventSender sender,
        ParkArea parkArea,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.ParkAreas.AnyAsync(pa => pa.Id == parkArea.Id, cancellationToken: cancellationToken);
        var response = new ParkAreaUpdatedPayload();

        throw new NotImplementedException();
    }
}

public class ParkAreaUpdatedPayload
{
    public ParkAreaUpdatedPayloadType Type { get; set; } = ParkAreaUpdatedPayloadType.Unchanged;
}

public enum ParkAreaUpdatedPayloadType
{
    Unchanged = 0,
    Created = 1,
    Updated = 2
}