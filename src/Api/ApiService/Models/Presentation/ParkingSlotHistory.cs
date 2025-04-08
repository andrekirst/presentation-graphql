namespace ApiService.Models.Presentation;

public class ParkingSlotHistory
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    public ParkArea ParkArea { get; set; } = null!;
}