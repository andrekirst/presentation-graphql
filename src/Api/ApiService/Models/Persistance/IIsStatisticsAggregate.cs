namespace ApiService.Models.Persistance;

public interface IIsStatisticsAggregateEntity
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public double Average { get; set; }
}