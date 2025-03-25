using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.Models.Database;
using ParkplatzDresden.ApiService.Models.Domain;

namespace ParkplatzDresden.ApiService.GraphQL;

[ExtendObjectType(KnownTypeNames.Mutation)]
public class ParkAreaMutations
{
    public async Task<ParkAreaUpdatedPayload> UpdateParkArea(
        ParkplatzDbContext dbContext,
        ParkArea parkArea,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.ParkAreas.AnyAsync(pa => pa.Id == parkArea.Id, cancellationToken: cancellationToken);
        var response = new ParkAreaUpdatedPayload();

        if (exists)
        {
            var entity = await dbContext.ParkAreas.SingleAsync(pa => pa.Id == parkArea.Id, cancellationToken);
            entity.DisplayName = parkArea.DisplayName;

            dbContext.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            response.Type = ParkAreaUpdatedPayloadType.Updated;

            return response;
        }

        dbContext.ParkAreas.Add(new ParkAreaEntity
        {
            Id = parkArea.Id,
            DisplayName = parkArea.DisplayName
        });
        await dbContext.SaveChangesAsync(cancellationToken);
        
        response.Type = ParkAreaUpdatedPayloadType.Created;
        return response;
    }
}

public class ParkAreaUpdatedPayload
{
    public ParkAreaUpdatedPayloadType Type { get; set; }
}

public enum ParkAreaUpdatedPayloadType
{
    Created = 1,
    Updated = 2
}