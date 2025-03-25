namespace ParkplatzDresden.ApiService.Models.Database;

public class ServiceTimeEntity : BaseEntity
{
    public bool IsAllDayOpen { get; set; }
    public TimeOnly? Opening { get; set; }
    public TimeOnly? Closing { get; set; }
    
    public int ParkAreaEntityId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}