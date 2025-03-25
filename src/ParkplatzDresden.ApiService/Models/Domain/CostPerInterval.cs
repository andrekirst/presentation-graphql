using ParkplatzDresden.ApiService.Models.Shared;

namespace ParkplatzDresden.ApiService.Models.Domain;

public class CostPerInterval
{
    public CostPerIntervalType Type { get; set; }
    public int Interval { get; set; }
    public double Costs { get; set; }
}