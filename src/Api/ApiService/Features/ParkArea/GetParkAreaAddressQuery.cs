using ApiService.Database;
using ApiService.Models.Presentation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Location = ApiService.Models.Persistance.Location;

namespace ApiService.Features.ParkArea;

public class GetParkAreaAddressQuery : IRequest<Address?>
{
    public Models.Presentation.ParkArea ParkArea { get; }

    public GetParkAreaAddressQuery(Models.Presentation.ParkArea parkArea)
    {
        ParkArea = parkArea;
    }
}

public class GetParkAreaAddressQueryHandler(IDbContextFactory<AppDbContext> dbContextFactory) : IRequestHandler<GetParkAreaAddressQuery, Address?>
{
    public async Task<Address?> Handle(GetParkAreaAddressQuery request, CancellationToken cancellationToken)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var result = await dbContext.Addresses
            .AsNoTracking()
            .Where(a => a.ParkAreaId == request.ParkArea.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new Address
        {
            Street = result?.Street,
            Number = result?.Number,
            PostalCode = result?.PostalCode,
            City = result?.City,
            Location = result?.Location ?? Location.Zero
        };
    }
}