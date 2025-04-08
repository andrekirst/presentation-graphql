using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("Regions")]
public class RegionEntity : BaseEntity
{
    [MaxLength(256)]
    public string DisplayName { get; set; } = null!;
    
    public ICollection<ParkAreaEntity> ParkAreas { get; set; } = null!;
}