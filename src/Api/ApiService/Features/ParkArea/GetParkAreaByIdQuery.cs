using ApiService.Database;
using ApiService.Validators;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Features.ParkArea;

public class GetParkAreaByIdQuery(int id) : IRequest<Models.Presentation.ParkArea>
{
    public int Id { get; set; } = id;
}

public class GetParkAreaByIdQueryValidator : AbstractValidator<GetParkAreaByIdQuery>
{
    public GetParkAreaByIdQueryValidator(ParkAreaIdValidator parkAreaIdValidator)
    {
        RuleFor(r => new ParkAreaIdValidatorParameters(r.Id))
            .SetValidator(parkAreaIdValidator);
    }
}

public class GetParkAreaByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetParkAreaByIdQuery, Models.Presentation.ParkArea>
{
    public Task<Models.Presentation.ParkArea> Handle(GetParkAreaByIdQuery request, CancellationToken cancellationToken)
    {
        return dbContext.ParkAreas
            .AsNoTracking()
            .Where(pa => pa.Id == request.Id)
            .Select(pa => new Models.Presentation.ParkArea
            {
                DisplayName = pa.DisplayName ?? string.Empty,
                Free = pa.Free,
                LastUpdate = pa.LastUpdate,
                Total = pa.Total
            })
            .SingleAsync(cancellationToken);
    }
}