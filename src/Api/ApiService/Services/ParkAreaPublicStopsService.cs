namespace ApiService.Services;

public class ParkAreaPublicStopsService
{
    private readonly Dictionary<int, string> _stops = new()
    {
        { 431, "33000004" },
        { 403, "33000004" },
        { 422, "33000004" },
        { 409, "33000037" }
    };

    public string? GetStopIds(int parkAreaId)
    {
        return _stops.SingleOrDefault(stop => parkAreaId == stop.Key).Value;
    }
}