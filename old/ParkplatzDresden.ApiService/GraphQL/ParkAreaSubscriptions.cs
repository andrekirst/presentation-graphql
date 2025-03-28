namespace ParkplatzDresden.ApiService.GraphQL;

[ExtendObjectType(KnownTypeNames.Subscriptions)]
public class ParkAreaSubscriptions
{
    [Subscribe]
    [Topic("ParkingSlotsUpdated-{id}")]
    public ParkingSlotsUpdatedPayload ParkingSlotsUpdated(int id, [EventMessage] ParkingSlotsUpdateEvent eventMessage)
    {
        return new ParkingSlotsUpdatedPayload
        {
            ParkAreaId = id
        };
    }
}

public class ParkingSlotsUpdateEvent
{
    public required int ParkAreaId { get; set; }
}

public class ParkingSlotsUpdatedPayload
{
    public required int ParkAreaId { get; set; }
}