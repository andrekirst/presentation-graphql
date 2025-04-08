namespace ApiService.GraphQL.Types;

public class ParkAreaSubscriptions
{
    [Subscribe]
    [Topic("ParkingSlotsChanged-{id}")]
    public ParkingSlotsChangedPayload ParkingSlotsChanged(int id, [EventMessage] ParkingSlotsChangedPayload payload) => payload;

    [Subscribe]
    [Topic("ParkingStateChanged-{id}")]
    public ParkingStateChangedPayload ParkingStateChanged(int id, [EventMessage] ParkingStateChangedPayload payload) => payload;
}

public class ParkingStateChangedPayload
{
    public string Icon { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class ParkingSlotsChangedPayload
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    public DateTime LastUpdate { get; set; }
    public int Change { get; set; }
}