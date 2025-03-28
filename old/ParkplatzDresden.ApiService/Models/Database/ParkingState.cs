namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkingState : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;

    public ICollection<ParkArea>? ParkAreas { get; set; }
}