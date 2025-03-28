using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ParkplatzDresden.ScraperLib;

public class Scraper
{
    public async Task<List<ParkArea>> ScrapeAsync(
        Func<Task<HtmlDocument>> sourceStartPage,
        Func<string, Task<HtmlDocument>> sourceSubsite,
        CancellationToken cancellationToken = default)
    {
        var document = await sourceStartPage();

        List<ParkArea> parkAreas = [];

        var parkAreasNodes = document.DocumentNode.SelectNodes("//tr");

        if (parkAreasNodes.Count == 0)
        {
            return [];
        }

        foreach (var parkAreasNode in parkAreasNodes)
        {
            var displayNameNode = parkAreasNode.SelectSingleNode(".//td[@class='left mobile_left']/div[@class='content']/a");

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

            var parkArea = new ParkArea
            {
                Id = id!.Value,
                DisplayName = displayName
            };

            var subsiteDocument = await sourceSubsite(href);
            LoadDataFromSubsite(parkArea, subsiteDocument);
            
            parkAreas.Add(parkArea);
        }

        return parkAreas;
    }

    private void LoadDataFromSubsite(ParkArea parkArea, HtmlDocument subsiteDocument)
    {
        var dataRows = subsiteDocument.DocumentNode.SelectNodes("//div[@class='row data-row']");

        foreach (var dataRow in dataRows)
        {
            var divsWithClasses = dataRow.ChildNodes.Where(c => c.HasClass("column")).ToList(); 
            
            if (divsWithClasses.Count == 2)
            {
                var text = divsWithClasses[0]?.InnerText;
                var value = divsWithClasses[1]?.InnerText;

                switch (text)
                {
                    case "Stellplätze:":
                    {
                        parkArea.ParkingSlot.Total = int.Parse(value!);
                        break;
                    }
                    case "davon frei:":
                    {
                        parkArea.ParkingSlot.Free = int.Parse(value!);
                        break;
                    }
                }
            }
        }
        
    }
}

public class ParkArea
{
    public int Id { get; set; }
    public string? DisplayName { get; set; }

    public ParkingSlot ParkingSlot { get; set; } = new();
}

public class ParkingSlot
{
    public int? Total { get; set; }
    public int? Free { get; set; }
}