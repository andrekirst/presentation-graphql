namespace ParkplatzDresden.ApiService.Models.Domain;

public class ServiceTime
{
    public bool IsAllDayOpen { get; set; }
    public TimeOnly Opening { get; set; }
    public TimeOnly Closing { get; set; }
}