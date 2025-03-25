namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingStateEntity : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public int ParkAreaEntityId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}