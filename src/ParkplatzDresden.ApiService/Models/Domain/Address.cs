namespace ParkplatzDresden.ApiService.Models.Domain;

public class Address
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public Location? Location { get; set; }
}