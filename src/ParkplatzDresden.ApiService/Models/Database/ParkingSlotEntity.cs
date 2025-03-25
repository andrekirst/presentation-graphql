namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingSlotEntity : BaseEntity
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    public int? Used { get; set; }
    
    public int ParkAreaEntityId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}