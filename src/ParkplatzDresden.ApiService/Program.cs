using Microsoft.EntityFrameworkCore;
using ParkplatzDresden.ApiService;
using ParkplatzDresden.ApiService.Database;
using ParkplatzDresden.ApiService.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name(KnownTypeNames.Query))
    .AddType<ParkAreaQueries>()
    .AddMutationType(m => m.Name(KnownTypeNames.Mutation))
    .AddType<ParkAreaMutations>();

builder.Services
    .AddDbContext<ParkplatzDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapDefaultEndpoints();
app.MapGraphQL();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ParkplatzDbContext>();
await dbContext.Database.MigrateAsync();

await app.RunAsync();