using ParkplatzDresden.ApiService.Models.Domain;

namespace ParkplatzDresden.ApiService.GraphQL;

[ExtendObjectType(KnownTypeNames.Query)]
public class ParkAreaQueries
{
    public async Task<ParkArea> GetParkArea(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ParkArea>> GetParkAreas(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}