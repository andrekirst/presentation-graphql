using ParkplatzDresden.ScraperLib;
using ParkplatzDresden.ScraperService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<Scraper>();
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