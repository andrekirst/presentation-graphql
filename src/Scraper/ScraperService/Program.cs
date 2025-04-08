using ScraperLibrary;
using ScraperService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var graphQlConnectionString = builder.Configuration.GetConnectionString("graphql");
ArgumentException.ThrowIfNullOrEmpty(graphQlConnectionString);

builder.Services.AddHostedService<ScrapingBackgroundService>();
builder.Services.AddSingleton<Scraper>();

builder.Services
    .AddParkplatzClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(graphQlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

