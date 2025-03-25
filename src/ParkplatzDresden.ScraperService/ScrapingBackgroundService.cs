using HtmlAgilityPack;
using ParkplatzDresden.ScraperLib;
using ScraperService.Api;

namespace ParkplatzDresden.ScraperService;

public class ScrapingBackgroundService(
    Scraper scraper,
    ParkplatzDresdenClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            const string url = "https://www.dresden.de/apps_ext/ParkplatzApp/index";

            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url, stoppingToken);
            
            var result = await scraper.ScrapeAsync(() => Task.FromResult(document), stoppingToken);

            foreach (var item in result)
            {
                await client.UpdateParkArea.ExecuteAsync(new ParkAreaInput
                {
                    DisplayName = item.DisplayName ?? "<ERROR>",
                    Id = item.Id
                }, stoppingToken);
            }
        }
    }
}