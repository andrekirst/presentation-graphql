namespace ParkplatzDresden.ApiService.Models.Database;

public class Costs : BaseEntity
{
    public float? CostsOnLoss { get; set; }
    public List<Fee> Fees { get; set; } = [];
}