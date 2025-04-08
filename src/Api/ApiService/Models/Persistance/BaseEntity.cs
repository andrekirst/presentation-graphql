using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Persistance;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ChangedAt { get; set; }
}