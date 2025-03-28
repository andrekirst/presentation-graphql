using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ParkplatzDresden.ApiService.Models.Database;

[Owned]
public class Address
{
    [MaxLength(256)]
    public string? Street { get; set; }
    
    [MaxLength(256)]
    public string? Number { get; set; }
    
    [MaxLength(256)]
    public string? PostalCode { get; set; }

    [MaxLength(256)]
    public string City { get; set; } = null!;
    
    public Location? Location { get; set; }
}