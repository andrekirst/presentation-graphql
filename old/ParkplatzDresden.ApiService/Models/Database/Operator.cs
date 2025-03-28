using System.ComponentModel.DataAnnotations;

namespace ParkplatzDresden.ApiService.Models.Database;

public class Operator : BaseEntity
{
    [MaxLength(256)]
    public string Identifier { get; set; } = null!;
    
    [MaxLength(256)]
    public string? Website { get; set; }
    
    [MaxLength(256)]
    public string? Email { get; set; }

    public ICollection<ParkArea> ParkAreas { get; set; } = null!;
}