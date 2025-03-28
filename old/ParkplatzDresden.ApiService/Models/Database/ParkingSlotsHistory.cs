namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingSlotsHistory : BaseEntity
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    
    public int ParkAreaId { get; set; }
    public ParkArea ParkArea { get; set; } = null!;
}