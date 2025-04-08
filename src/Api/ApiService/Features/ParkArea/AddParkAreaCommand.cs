using ApiService.Database;
using ApiService.GraphQL.Types;
using ApiService.Models.Persistance;
using FluentValidation;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Location = ApiService.Models.Persistance.Location;

namespace ApiService.Features.ParkArea;

public class ParkAreaAddedPayload
{
    public ChangeStatus ChangeStatus { get; set; }
}

public enum ChangeStatus
{
    Unmodified = 0,
    Created = 1,
    Modified = 2
}

public class AddParkAreaPayload
{
    public int ParkAreaId { get; set; }
    public string DisplayName { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }

    public string? ParkingStateName { get; set; }
    public string? ParkingStateIcon { get; set; }

    public string? OperatorWebsite { get; set; }
    public string? OperatorEmail { get; set; }

    public string? AddressStreet { get; set; }
    public string? AddressNumber { get; set; }
    public string? AddressPostalCode { get; set; }
    public string? AddressCity { get; set; }
    public Location? Location { get; set; }

    public bool ServiceTimeIsAllDayOpen { get; set; }
    public TimeOnly? ServiceTimeOpening { get; set; }
    public TimeOnly? ServiceTimeClosing { get; set; }
    
    public string RegionDisplayName { get; set; } = null!;
}

public class AddParkAreaPayloadValidator : AbstractValidator<AddParkAreaPayload>
{
    public AddParkAreaPayloadValidator()
    {
        // TODO Add validations
    }
}

public class AddParkAreaCommand(AddParkAreaPayload payload) : IRequest<ParkAreaAddedPayload>
{
    public AddParkAreaPayload Payload { get; } = payload;
}

public class AddParkAreaCommandHandler(
    AppDbContext dbContext,
    ITopicEventSender sender) : IRequestHandler<AddParkAreaCommand, ParkAreaAddedPayload>
{
    public async Task<ParkAreaAddedPayload> Handle(AddParkAreaCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var parkAreaId = payload.ParkAreaId;
        var entity = await dbContext.ParkAreas
            .Include(pa => pa.ParkingState)
            .Include(pa => pa.Address)
            .Include(pa => pa.Operator)
            .Include(pa => pa.Region)
            .Include(pa => pa.ServiceTime)
            .Where(pa => pa.Id == parkAreaId)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            return await CreateAsync(parkAreaId, payload, cancellationToken);
        }

        return await UpdateAsync(entity, parkAreaId, payload, cancellationToken);
    }

    private async Task<ParkAreaAddedPayload> UpdateAsync(ParkAreaEntity entity, int parkAreaId, AddParkAreaPayload payload, CancellationToken cancellationToken)
    {
        var hasParkingSlotChanged = await DetectChangeForParkingSlots(entity, parkAreaId, payload, cancellationToken);
        var hasParkingStateChanged = await DetechChangeForParkingState(entity, parkAreaId, payload, cancellationToken);

        var hasChanged = hasParkingSlotChanged || hasParkingStateChanged; 
            
        if (!hasChanged)
        {
            return new ParkAreaAddedPayload
            {
                ChangeStatus = ChangeStatus.Unmodified
            };
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ParkAreaAddedPayload
        {
            ChangeStatus = ChangeStatus.Modified
        };
    }

    private async Task<bool> DetechChangeForParkingState(ParkAreaEntity entity, int parkAreaId, AddParkAreaPayload payload, CancellationToken cancellationToken)
    {
        if (entity.ParkingState != null &&
            entity.ParkingState?.Name == payload.ParkingStateName &&
            entity.ParkingState?.Icon == payload.ParkingStateIcon)
        {
            return false;
        }

        if (entity.ParkingState is null)
        {
            entity.ParkingState = new ParkingStateEntity
            {
                Icon = payload.ParkingStateIcon!,
                Name = payload.ParkingStateName!
            };
        }
        else
        {
            var parkingStateId = await dbContext.ParkingStates
                .Where(ps => ps.Icon == payload.ParkingStateIcon)
                .Select(ps => ps.Id)
                .FirstAsync(cancellationToken);

            entity.ParkingStateId = parkingStateId;
        }

        dbContext.ParkAreas.Update(entity);
            
        await sender.SendAsync(
            $"ParkingStateChanged-{parkAreaId}",
            new ParkingStateChangedPayload
            {
                Icon = payload.ParkingStateIcon!,
                Name = payload.ParkingStateName!
            },
            cancellationToken);

        return true;

    }

    private async Task<bool> DetectChangeForParkingSlots(ParkAreaEntity entity, int parkAreaId, AddParkAreaPayload payload, CancellationToken cancellationToken)
    {
        if (entity.Total == payload.Total && entity.Free == payload.Free)
        {
            return false;
        }

        var change = (payload.Free ?? 0) - (entity.Free ?? 0);

        entity.Total = payload.Total;
        entity.Free = payload.Free;
        
        await sender.SendAsync(
            $"ParkingSlotsChanged-{parkAreaId}",
            new ParkingSlotsChangedPayload
            {
                Total = payload.Total,
                Free = payload.Free,
                LastUpdate = payload.LastUpdate,
                Change = change
            },
            cancellationToken);

        dbContext.ParkAreas.Update(entity);
        dbContext.ParkingSlotHistories.Add(new ParkingSlotHistoryEntity
        {
            ParkAreaId = parkAreaId,
            Total = payload.Total,
            Free = payload.Free
        });

        return true;
    }

    private async Task<ParkAreaAddedPayload> CreateAsync(int parkAreaId, AddParkAreaPayload payload, CancellationToken cancellationToken)
    {
        var parkingState = await dbContext.ParkingStates.SingleOrDefaultAsync(ps => ps.Icon == payload.ParkingStateIcon, cancellationToken)
                           ?? new ParkingStateEntity
                           {
                               Icon = payload.ParkingStateIcon!,
                               Name = payload.ParkingStateName!
                           };

        var @operator = await dbContext.Operators.SingleOrDefaultAsync(o => o.Email == payload.OperatorEmail || o.Website == payload.OperatorWebsite, cancellationToken)
                        ?? new OperatorEntity
                        {
                            Email = payload.OperatorEmail,
                            Website = payload.OperatorWebsite
                        };

        var region = await dbContext.Regions.SingleOrDefaultAsync(r => r.DisplayName == payload.RegionDisplayName, cancellationToken)
                     ?? new RegionEntity
                     {
                         DisplayName = payload.RegionDisplayName
                     };

        var entity = new ParkAreaEntity
        {
            Id = parkAreaId,
            DisplayName = payload.DisplayName,
            Free =  payload.Free,
            LastUpdate = payload.LastUpdate,
            Total = payload.Total,
            Address = new AddressEntity
            {
                City = payload.AddressCity,
                Number = payload.AddressNumber,
                PostalCode = payload.AddressPostalCode,
                Street = payload.AddressStreet,
                Location = payload.Location ?? Location.Zero
            },
            Operator = @operator,
            Region = region,
            ParkingState = parkingState,
            ServiceTime = new ServiceTimeEntity
            {
                Closing = payload.ServiceTimeClosing,
                Opening = payload.ServiceTimeClosing,
                IsAllDayOpen = payload.ServiceTimeIsAllDayOpen
            }
        };

        dbContext.ParkAreas.Add(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ParkAreaAddedPayload
        {
            ChangeStatus = ChangeStatus.Created
        };
    }
}

