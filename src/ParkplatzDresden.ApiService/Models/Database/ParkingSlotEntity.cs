namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingSlotsEntity : BaseEntity
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    
    public int ParkAreaEntityId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}