using ApiService.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Validators;

public class ParkAreaIdValidator : AbstractValidator<ParkAreaIdValidatorParameters>
{
    public ParkAreaIdValidator(AppDbContext dbContext)
    {
        RuleFor(r => r.Id)
            .MustAsync((id, token) => dbContext.ParkAreas.AsNoTracking().AnyAsync(pa => pa.Id == id, token))
            .WithMessage(parameters => $"ParkArea with id \"{parameters.Id}\" not found");
    }
}

public class ParkAreaIdValidatorParameters(int id)
{
    public int Id { get; set; } = id;
}