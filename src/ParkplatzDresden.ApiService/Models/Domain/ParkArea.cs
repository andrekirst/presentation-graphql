namespace ParkplatzDresden.ApiService.Models.Domain;

public class ParkArea
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public ParkingSlots? ParkingSlots { get; set; }
    // public ParkingState? ParkingState { get; set; }
    // public string? Trend { get; set; }
    // public DateTimeOffset LastUpdate { get; set; }
    // public Address? Address { get; set; }
    // public ServiceTime? ServiceTime { get; set; }
    // public Costs? Costs { get; set; }
    // public Operator? Operator { get; set; }
    // public string Region { get; set; } = null!;
}