namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingSlotsHistoryEntity : BaseEntity
{
    public int? Total { get; set; }
    public int? Free { get; set; }
    
    public int ParkAreaId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}