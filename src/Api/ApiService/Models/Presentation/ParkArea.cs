namespace ApiService.Models.Presentation;

public class ParkArea : Model
{
    public string DisplayName { get; set; } = null!;
    public DateTime? LastUpdate { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }
    
    public Address? Address { get; set; }

    // TODO
    // public Statistics GetStatistics() => new Statistics();
}

// public class Statistics
// {
//     public YearStatistic GetYearStatistic(int year) => new YearStatistic(year);
// }
//
// public class YearStatistic(int year)
// {
// }
