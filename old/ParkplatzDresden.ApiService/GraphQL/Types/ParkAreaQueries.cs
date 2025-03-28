using ParkplatzDresden.ApiService.Models.Database;
using ParkplatzDresden.ApiService.Services;

namespace ParkplatzDresden.ApiService.GraphQL.Types;

[QueryType]
public static partial class ParkAreaQueries
{
    [UseProjection]
    public static async Task<ParkArea?> GetParkArea(
        int id,
        ParkAreaService parkAreaService,
        CancellationToken cancellationToken) =>
        await parkAreaService.GetParkAreaByIdAsync(id, cancellationToken);
    
    public static async Task<List<ParkArea>> GetParkAreas(
        ParkAreaService parkAreaService, CancellationToken cancellationToken) =>
        await parkAreaService.GetParkAreasAsync(cancellationToken);
}