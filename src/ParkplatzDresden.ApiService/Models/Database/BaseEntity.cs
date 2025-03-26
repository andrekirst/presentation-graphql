using System.ComponentModel.DataAnnotations;

namespace ParkplatzDresden.ApiService.Models.Database;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ChangedAt { get; set; }
}