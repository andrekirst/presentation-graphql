using ApiService.Database;
using ApiService.Database.Interceptors;
using ApiService.GraphQL.Types;
using ApiService.Infrastructure;
using ApiService.Services;
using ApiService.Validators;
using DvbCs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidationException = ApiService.Exceptions.ValidationException;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<AppDbContext>();
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining<AppDbContext>();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("db"))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();

    options.AddInterceptors(new AuditInterceptor());
});

builder.Services.AddHostedService<MigrateDatabaseService>();

builder.Services.AddDvbServices();
builder.Services.AddSingleton<ParkAreaPublicStopsService>();

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .RegisterDbContextFactory<AppDbContext>()
    .AddQueryType<ParkAreaQueries>()
    .AddMutationType<ParkAreaMutations>()
    .AddSubscriptionType<ParkAreaSubscriptions>()
    .AddType<ParkAreaType>()
    .AddSorting();

builder.Services.AddErrorFilter(error =>
{
    if (error.Exception is not ValidationException validationException)
    {
        return error;
    }

    var firstError = validationException.Errors?.FirstOrDefault();

    return firstError is not null ? error.WithMessage(firstError.ErrorMessage) : error;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseWebSockets();
app.MapGraphQL();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.Run();