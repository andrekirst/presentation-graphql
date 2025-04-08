using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("ParkingSlotHistories")]
public sealed class ParkingSlotHistoryEntity : BaseEntity
{
    public int? Total { get; set; }
    public int? Free { get; set; }

    public int ParkAreaId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}