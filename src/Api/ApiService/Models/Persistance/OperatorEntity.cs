using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("Operators")]
public class OperatorEntity : BaseEntity
{
    [MaxLength(256)]
    public string? Website { get; set; }
    
    [MaxLength(256)]
    public string? Email { get; set; }

    public ICollection<ParkAreaEntity> ParkAreas { get; set; } = null!;
}