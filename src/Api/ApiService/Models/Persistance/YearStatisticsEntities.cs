using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("YearStatisticsAggregates")]
public class YearStatisticsAggregateEntity : YearStatisticsBaseEntity, IIsStatisticsAggregateEntity
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public double Average { get; set; }
}

[Table("YearStatisticsValues")]
public class YearStatisticsValuesEntity : YearStatisticsBaseEntity, IIsStatisticsValuesEntity
{
    public int Value { get; set; }
    public int Count { get; set; }
}