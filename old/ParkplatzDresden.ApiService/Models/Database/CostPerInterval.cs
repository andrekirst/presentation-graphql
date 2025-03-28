using ParkplatzDresden.ApiService.Models.Shared;

namespace ParkplatzDresden.ApiService.Models.Database;

public class CostPerInterval
{
    public CostPerIntervalType Type { get; set; }
    public int Interval { get; set; }
    public float? Costs { get; set; }
}