using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails();
builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name(KnownTypeNames.Query))
    .AddType<ParkAreaQueries>()
    .AddMutationType(m => m.Name(KnownTypeNames.Mutation))
    .AddType<ParkAreaMutations>()
    .AddSubscriptionType(s => s.Name(KnownTypeNames.Subscriptions))
    .AddType<ParkAreaSubscriptions>()
    .AddInMemorySubscriptions();

builder.Services
    .AddDbContext<ParkplatzDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
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
var dbContext = scope.ServiceProvider.GetRequiredService<ParkplatzDbContext>();
await dbContext.Database.MigrateAsync();

await app.RunAsync();