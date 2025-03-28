using ParkplatzDresden.ScraperLib;
using ParkplatzDresden.ScraperService;
using ScraperService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddSingleton<Scraper>();
builder.Services
    .AddParkplatzDresdenClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://apiservice/graphql"));

builder.Services.AddHostedService<ScrapingBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/status", () => "test")
    .WithName("GetStatus");

app.Run();