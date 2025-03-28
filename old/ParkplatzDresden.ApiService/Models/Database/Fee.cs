using ParkplatzDresden.ApiService.Models.Shared;

namespace ParkplatzDresden.ApiService.Models.Database;

public class Fee
{
    public FeeType Type { get; set; }
    public string[] Weekdays { get; set; } = [];
    public TimeRange? TimeRange { get; set; }
    public CostPerInterval? CostPerInterval { get; set; }
    public bool IsWholeDay { get; set; }
    public bool IncludeHolidays { get; set; }
}