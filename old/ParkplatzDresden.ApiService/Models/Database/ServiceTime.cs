using Microsoft.EntityFrameworkCore;

namespace ParkplatzDresden.ApiService.Models.Database;

[Owned]
public class ServiceTime
{
    public bool IsAllDayOpen { get; set; }
    public TimeOnly? Opening { get; set; }
    public TimeOnly? Closing { get; set; }
}