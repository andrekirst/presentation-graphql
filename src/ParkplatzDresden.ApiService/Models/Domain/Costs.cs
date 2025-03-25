namespace ParkplatzDresden.ApiService.Models.Domain;

public class Costs
{
    public double? CostsOnLoss { get; set; }
    public List<Fee> Fees { get; set; } = [];
}