using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Models.Persistance;

[Table("Addresses")]
public class AddressEntity : BaseEntity
{
    [MaxLength(256)]
    public string? Street { get; set; }
    
    [MaxLength(256)]
    public string? Number { get; set; }
    
    [MaxLength(256)]
    public string? PostalCode { get; set; }
    
    [MaxLength(256)]
    public string? City { get; set; }

    public Location Location { get; set; }

    public int ParkAreaId { get; set; }
    public ParkAreaEntity ParkArea { get; set; } = null!;
}

[Owned]
public record Location(double Latitude, double Longitute)
{
    public static Location Zero => new(0, 0);
}