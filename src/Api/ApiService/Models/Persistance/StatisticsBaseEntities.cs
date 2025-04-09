namespace ApiService.Models.Persistance;

public class StatisticsBaseEntity : BaseEntity
{
    public ParkAreaEntity ParkArea { get; set; } = null!;
    public int ParkAreaId { get; set; }
}

public class YearStatisticsBaseEntity : StatisticsBaseEntity
{
    public int Year { get; set; }
}

public class MonthStatisticsBaseEntity : YearStatisticsBaseEntity
{
    public int Month { get; set; }
}

public class DayStatisticsBaseEntity : MonthStatisticsBaseEntity
{
    public int Day { get; set; }
}

public class HourStatisticsBaseEntity : DayStatisticsBaseEntity
{
    public int Hour { get; set; }
}