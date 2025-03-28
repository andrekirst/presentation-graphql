using System.ComponentModel.DataAnnotations;

namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkArea : BaseEntity
{
    [MaxLength(256)]
    public string DisplayName { get; set; } = null!;
    
    public DateTime LastUpdate { get; set; }
    
    [MaxLength(256)]
    public string? Trend { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }

    public ICollection<ParkingSlotsHistory>? ParkingSlotsHistory { get; set; }
    public int? ParkingStateId { get; set; }
    public ParkingState? ParkingState { get; set; }
    
    public Address? Address { get; set; }
    public ServiceTime? ServiceTime { get; set; }
    
    public int? OperatorId { get; set; }
    public Operator? Operator { get; set; }

    public int RegionId { get; set; }
    public Region Region { get; set; } = null!;
}