using System.ComponentModel.DataAnnotations.Schema;

namespace ApiService.Models.Persistance;

[Table("MonthStatisticsAggregates")]
public class MonthStatisticsAggregateEntity : MonthStatisticsBaseEntity, IIsStatisticsAggregateEntity
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public double Average { get; set; }
}

[Table("MonthStatisticsValues")]
public class MonthStatisticsValuesEntity : MonthStatisticsBaseEntity, IIsStatisticsValuesEntity
{
    public int Value { get; set; }
    public int Count { get; set; }
}