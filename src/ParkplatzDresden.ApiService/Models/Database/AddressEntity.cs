namespace ParkplatzDresden.ApiService.Models.Database;

public class AddressEntity : BaseEntity
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public Location? Location { get; set; }
    
    public int ParkAreaEntityId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}