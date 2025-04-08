using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("ServiceTimes")]
public class ServiceTimeEntity : BaseEntity
{
    public bool IsAllDayOpen { get; set; }
    public TimeOnly? Opening { get; set; }
    public TimeOnly? Closing { get; set; }

    public int ParkAreaId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}