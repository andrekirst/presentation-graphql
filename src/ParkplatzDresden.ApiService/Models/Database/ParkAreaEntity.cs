using System.ComponentModel.DataAnnotations;

namespace ParkplatzDresden.ApiService.Models.Database;

public class ParkAreaEntity : BaseEntity
{
    [MaxLength(256)]
    public string DisplayName { get; set; } = null!;
    // public DateTime LastUpdate { get; set; }
    // public string? Trend { get; set; }
    //
    public ParkingSlotsEntity? ParkingSlot { get; set; }

    public ICollection<ParkingSlotsHistoryEntity>? ParkingSlotsHistory { get; set; }
    //
    // public int ParkingStateEntityId { get; set; }
    // public ParkingSlotEntity ParkingState { get; set; } = null!;
    //
    // public int? AddressEntityId { get; set; }
    // public AddressEntity? Address { get; set; } = null!;
    //
    // public int? ServiceTimeEntityId { get; set; }
    // public ServiceTimeEntity? ServiceTime { get; set; }
    //
    // public int? OperatorEntityId { get; set; }
    // public OperatorEntity? Operator { get; set; }
}