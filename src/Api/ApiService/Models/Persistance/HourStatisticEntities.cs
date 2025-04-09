using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("HourStatisticsAggregates")]
public class HourStatisticsAggregateEntity : HourStatisticsBaseEntity, IIsStatisticsAggregateEntity
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public double Average { get; set; }
}

[Table("HourStatisticsValues")]
public class HourStatisticsValuesEntity : HourStatisticsBaseEntity, IIsStatisticsValuesEntity
{
    public int Value { get; set; }
    public int Count { get; set; }
}