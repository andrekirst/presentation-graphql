namespace ParkplatzDresden.ApiService.Models.Database;

public class CostsEntity : BaseEntity
{
    public float? CostsOnLoss { get; set; }
    public List<FeeEntity> Fees { get; set; } = [];
}