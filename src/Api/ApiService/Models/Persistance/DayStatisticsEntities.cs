using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("DayStatisticsAggregates")]
public class DayStatisticsAggregateEntity : DayStatisticsBaseEntity, IIsStatisticsAggregateEntity
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public double Average { get; set; }
}

[Table("DayStatisticsValues")]
public class DayStatisticsValuesEntity : DayStatisticsBaseEntity, IIsStatisticsValuesEntity
{
    public int Value { get; set; }
    public int Count { get; set; }
}