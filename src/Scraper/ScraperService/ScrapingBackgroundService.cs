using HtmlAgilityPack;
using ScraperLibrary;
using ScraperService.GraphQL;

namespace ScraperService;

public class ScrapingBackgroundService(
    Scraper scraper,
    ParkplatzClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            const string url = "https://www.dresden.de/apps_ext/ParkplatzApp/index";
            var baseUri = new Uri(url);

            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url, stoppingToken);
            
            var iterator = scraper.ScrapeAsync(
                () => Task.FromResult(document),
                async href =>
                {
                    var fullUri = new Uri(baseUri, href);
                    return await web.LoadFromWebAsync(fullUri.ToString(), stoppingToken);
                }, stoppingToken);

            await foreach (var item in iterator)
            {
                await client.AddParkArea.ExecuteAsync(new AddParkAreaPayloadInput
                {
                    Free = item.Free,
                    Location = new LocationInput
                    {
                        Latitude = item.Location?.Latitude ?? 0,
                        Longitute = item.Location?.Longitute ?? 0
                    },
                    Total = item.Total,
                    AddressCity = item.AddressCity,
                    AddressNumber = item.AddressNumber,
                    AddressStreet = item.AddressStreet,
                    DisplayName = item.DisplayName,
                    LastUpdate = item.LastUpdate,
                    OperatorEmail = item.OperatorEmail,
                    OperatorWebsite = item.OperatorWebsite,
                    AddressPostalCode = item.AddressPostalCode,
                    ParkAreaId = item.ParkAreaId,
                    ParkingStateIcon = item.ParkingStateIcon,
                    ParkingStateName = item.ParkingStateName,
                    RegionDisplayName = item.RegionDisplayName,
                    ServiceTimeClosing = item.ServiceTimeClosing,
                    ServiceTimeOpening = item.ServiceTimeOpening,
                    ServiceTimeIsAllDayOpen = item.ServiceTimeIsAllDayOpen
                }, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }
}