using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.Services;

public class ParkAreaService(ParkplatzDbContext dbContext)
{
    public async Task<ParkArea?> GetParkAreaByIdAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        await dbContext.ParkAreas
            .AsNoTracking()
            .Where(pa => pa.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<List<ParkArea>> GetParkAreasAsync(CancellationToken cancellationToken = default) =>
        await dbContext.ParkAreas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
}