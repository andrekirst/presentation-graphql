using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.GraphQL;
using ParkplatzDresden.ApiService.GraphQL.Types;
using ParkplatzDresden.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddScoped<ParkAreaService>();

builder.Services.AddProblemDetails();
builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<ParkplatzDbContext>()
    .AddQueryType()
    .AddMutationType(m => m.Name(KnownTypeNames.Mutation))
    .AddType<ParkAreaMutations>()
    .AddSubscriptionType(s => s.Name(KnownTypeNames.Subscriptions))
    .AddType<ParkAreaSubscriptions>()
    .AddType<ParkAreaType>()
    .AddInMemorySubscriptions()
    .AddProjections();

builder.Services
    .AddDbContextFactory<ParkplatzDbContext>(optionsBuilder =>
    {
        optionsBuilder
            .UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
    });

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWebSockets();
app.MapDefaultEndpoints();
app.MapGraphQL();

using var scope = app.Services.CreateScope();
var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ParkplatzDbContext>>();
await using var dbContext = await dbContextFactory.CreateDbContextAsync();
await dbContext.Database.MigrateAsync();

await app.RunAsync();