using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ParkplatzDresden.ScraperLib;

public class Scraper
{
    public async Task<List<ParkArea>> ScrapeAsync(
        Func<Task<HtmlDocument>> source,
        CancellationToken cancellationToken = default)
    {
        var document = await source();

        List<ParkArea> parkAreas = [];

        var rows = document.DocumentNode.SelectNodes("//tr");

        if (rows.Count == 0)
        {
            return [];
        }

        foreach (var row in rows)
        {
            var displayNameNode = row.SelectSingleNode(".//td[@class='left mobile_left']/div[@class='content']/a");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (displayNameNode is null)
            {
                continue;
            }

            var displayName = displayNameNode.InnerText;
            int? id = null;

            var href = displayNameNode.GetAttributeValue("href", "");
            var idMatch = Regex.Match(href, @"id=(\d+)", RegexOptions.Compiled);

            if (idMatch.Success)
            {
                id = int.Parse(idMatch.Groups[1].Value);
            }
            
            var hasTotal = int.TryParse(row.SelectSingleNode(".//td[@class='center mobile_left']/div[@class='content'][preceding-sibling::div[text()='Stellplätze']]")?.InnerText, out var total);
            var hasFree = int.TryParse(row.SelectSingleNode(".//td[@class='center mobile_left']/div[@class='content'][preceding-sibling::div[text()='frei']]")?.InnerText, out var free);
            
            parkAreas.Add(new ParkArea
            {
                Id = id!.Value,
                DisplayName = displayName,
                ParkingSlot = new ParkingSlot
                {
                    Free = hasFree ? free : null,
                    Total = hasTotal ? total : null
                }
            });
        }

        return parkAreas;
    }
}

public class ParkArea
{
    public int Id { get; set; }
    public string? DisplayName { get; set; }

    public ParkingSlot? ParkingSlot { get; set; }
}

public class ParkingSlot
{
    public int? Total { get; set; }
    public int? Free { get; set; }
}