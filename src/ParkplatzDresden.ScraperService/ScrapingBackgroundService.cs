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
            var baseUri = new Uri(url);

            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url, stoppingToken);
            
            var result = await scraper.ScrapeAsync(
                () => Task.FromResult(document),
                async href =>
                {
                    var fullUri = new Uri(baseUri, href);
                    return await web.LoadFromWebAsync(fullUri.ToString(), stoppingToken);
                }, stoppingToken);

            foreach (var item in result)
            {
                await client.UpdateParkArea.ExecuteAsync(new ParkAreaInput
                {
                    DisplayName = item.DisplayName ?? "<ERROR>",
                    Id = item.Id,
                    ParkingSlots = new ParkingSlotsInput
                    {
                        Total = item.ParkingSlot.Total,
                        Free = item.ParkingSlot.Free
                    }
                }, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}