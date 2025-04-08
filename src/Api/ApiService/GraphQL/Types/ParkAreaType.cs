using ApiService.Models.Presentation;
// ReSharper disable PreferConcreteValueOverDefault

namespace ApiService.GraphQL.Types;

public class ParkAreaType : ObjectType<ParkArea>
{
    protected override void Configure(IObjectTypeDescriptor<ParkArea> descriptor)
    {
        descriptor
            .Field(f => f.Address)
            .ResolveWith<ParkAreaAddressResolvers>(a => ParkAreaAddressResolvers.GetAddressAsync(default!, default!, default!));
        
        descriptor
            .Field("publicTransport")
            .Argument("limit", a => a.Type<IntType>())
            .ResolveWith<ParkAreaPublicTransportationInformationResolvers>(r => r.GetNearbyPublicTransportInformationAsync(default!, default!, default!, default!));
    }
}