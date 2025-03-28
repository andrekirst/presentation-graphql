using System.ComponentModel.DataAnnotations;

namespace ParkplatzDresden.ApiService.Models.Database;

public class Region : BaseEntity
{
    [MaxLength(256)]
    public string Identifier { get; set; } = null!;
    
    [MaxLength(256)]
    public string DisplayName { get; set; } = null!;

    public ICollection<ParkArea> ParkAreas { get; set; } = null!;
}