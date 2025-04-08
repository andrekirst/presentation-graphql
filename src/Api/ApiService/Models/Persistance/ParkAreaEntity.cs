using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("ParkAreas")]
public sealed class ParkAreaEntity : BaseEntity
{
    [MaxLength(256)]
    public string? DisplayName { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }

    public ICollection<ParkingSlotHistoryEntity>? ParkingSlotHistories { get; set; }

    public int? ParkingStateId { get; set; }
    public ParkingStateEntity? ParkingState { get; set; }

    public AddressEntity? Address { get; set; }
    public ServiceTimeEntity? ServiceTime { get; set; }

    public int? OperatorId { get; set; }
    public OperatorEntity? Operator { get; set; }

    public int RegionId { get; set; }
    public RegionEntity Region { get; set; }
}