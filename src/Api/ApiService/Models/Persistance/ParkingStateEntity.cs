using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("ParkingStates")]
public class ParkingStateEntity : BaseEntity
{
    [MaxLength(256)]
    public string Name { get; set; } = null!;
    
    [MaxLength(256)]
    public string Icon { get; set; } = null!;

    public ICollection<ParkAreaEntity> ParkAreas { get; set; }
}