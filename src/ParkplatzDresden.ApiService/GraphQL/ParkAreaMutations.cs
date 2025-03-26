using HotChocolate.Subscriptions;
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
        ITopicEventSender sender,
        ParkArea parkArea,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.ParkAreas.AnyAsync(pa => pa.Id == parkArea.Id, cancellationToken: cancellationToken);
        var response = new ParkAreaUpdatedPayload();

        if (exists)
        {
            var entity = await dbContext.ParkAreas
                .Include(parkAreaEntity => parkAreaEntity.ParkingSlot)
                .SingleAsync(pa => pa.Id == parkArea.Id, cancellationToken);

            if (entity.DisplayName != parkArea.DisplayName)
            {
                entity.DisplayName = parkArea.DisplayName;
                response.Type = ParkAreaUpdatedPayloadType.Updated;
            }

            if (entity.ParkingSlot is null && parkArea.ParkingSlots is not null)
            {
                entity.ParkingSlot = new ParkingSlotsEntity
                {
                    Total = parkArea.ParkingSlots.Total,
                    Free = parkArea.ParkingSlots.Free
                };

                dbContext.ParkAreas.Update(entity);
                dbContext.ParkingSlotsHistories.Add(new ParkingSlotsHistoryEntity
                {
                    Free = parkArea.ParkingSlots.Free,
                    Total = parkArea.ParkingSlots.Total,
                    ParkAreaId = parkArea.Id
                });
                await sender.SendAsync(
                    $"{nameof(ParkAreaSubscriptions.ParkingSlotsUpdated)}-{parkArea.Id}",
                    new ParkingSlotsUpdateEvent
                    {
                        ParkingSlots = parkArea.ParkingSlots,
                        ParkAreaId = parkArea.Id
                    },
                    cancellationToken);
                response.Type = ParkAreaUpdatedPayloadType.Updated;
            }
            else if (entity.ParkingSlot is not null &&
                     parkArea.ParkingSlots is not null &&
                     (entity.ParkingSlot.Total != parkArea.ParkingSlots.Total ||
                     entity.ParkingSlot.Free != parkArea.ParkingSlots.Free))
            {
                entity.ParkingSlot.Total = parkArea.ParkingSlots.Total;
                entity.ParkingSlot.Free = parkArea.ParkingSlots.Free;
                
                dbContext.ParkAreas.Update(entity);
                dbContext.ParkingSlotsHistories.Add(new ParkingSlotsHistoryEntity
                {
                    Free = parkArea.ParkingSlots.Free,
                    Total = parkArea.ParkingSlots.Total,
                    ParkAreaId = parkArea.Id
                });

                await sender.SendAsync(
                    $"{nameof(ParkAreaSubscriptions.ParkingSlotsUpdated)}-{parkArea.Id}",
                    new ParkingSlotsUpdateEvent
                    {
                        ParkingSlots = parkArea.ParkingSlots,
                        ParkAreaId = parkArea.Id
                    },
                    cancellationToken);

                dbContext.Update(entity);
                response.Type = ParkAreaUpdatedPayloadType.Updated;
            }
            
            await dbContext.SaveChangesAsync(cancellationToken);

            return response;
        }

        dbContext.ParkAreas.Add(new ParkAreaEntity
        {
            Id = parkArea.Id,
            DisplayName = parkArea.DisplayName,
            ParkingSlot = new ParkingSlotsEntity
            {
                Free = parkArea.ParkingSlots?.Free,
                Total = parkArea.ParkingSlots?.Total
            }
        });
        dbContext.ParkingSlotsHistories.Add(new ParkingSlotsHistoryEntity
        {
            Free = parkArea.ParkingSlots?.Free,
            Total = parkArea.ParkingSlots?.Total,
            ParkAreaId = parkArea.Id
        });
        await dbContext.SaveChangesAsync(cancellationToken);
        
        response.Type = ParkAreaUpdatedPayloadType.Created;
        return response;
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