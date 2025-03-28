using Microsoft.EntityFrameworkCore;

namespace ParkplatzDresden.ApiService.Models.Database;

[Owned]
public class Location
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}