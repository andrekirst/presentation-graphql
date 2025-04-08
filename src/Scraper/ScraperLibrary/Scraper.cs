using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ScraperLibrary;

public class Scraper
{
    private static readonly CultureInfo DeDe = CultureInfo.GetCultureInfo("de-DE");
    private static readonly CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");
    
    public async IAsyncEnumerable<AddParkAreaPayload> ScrapeAsync(
        Func<Task<HtmlDocument>> sourceStartPage,
        Func<string, Task<HtmlDocument>> sourceSubsite,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var document = await sourceStartPage();

        List<AddParkAreaPayload> payloads = [];

        var parkAreasNodes = document.DocumentNode.SelectNodes("//tr");

        if (parkAreasNodes.Count == 0)
        {
            yield break;
        }

        var availableParkingStatesNodes = document.DocumentNode.SelectNodes("//div[starts-with(@class, 'park')]/@class");

        var parkingStates = (
            from availableParkingStatesNode in availableParkingStatesNodes
            let Icon = availableParkingStatesNode.Attributes.Single(a => a.Name == "class").Value
            let Name = availableParkingStatesNode.InnerText
            select (Icon, Name))
            .ToList();

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

            #region ParkingState

            var parkingStateNode = displayNameNode.ParentNode.ParentNode.ParentNode.SelectSingleNode(".//td");
            var parkingStateIconClasses = parkingStateNode?.Attributes?.SingleOrDefault(a => a.Name == "class")?.Value;
            string? parkingStateIcon = null;
            string? parkingStateName = null;

            if (parkingStateIconClasses is not null)
            {
                foreach (var parkingState in parkingStates.Where(parkingState => parkingStateIconClasses.Contains(parkingState.Icon)))
                {
                    parkingStateIcon = parkingState.Icon;
                    parkingStateName = parkingState.Name;
                    break;
                }
            }

            #endregion

            var regionDisplayName = displayNameNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.SelectSingleNode(".//thead/tr/th[2]/div").InnerText;
            
            var payload = new AddParkAreaPayload
            {
                ParkAreaId = id!.Value,
                DisplayName = displayName,
                ParkingStateIcon = parkingStateIcon,
                ParkingStateName = parkingStateName,
                RegionDisplayName = regionDisplayName
            };

            var subsiteDocument = await sourceSubsite(href);
            LoadDataFromSubsite(payload, subsiteDocument);

            yield return payload;
        }
    }

    private static void LoadDataFromSubsite(AddParkAreaPayload payload, HtmlDocument subsiteDocument)
    {
        #region row data-row

        var dataRows = subsiteDocument.DocumentNode.SelectNodes("//div[@class='row data-row']");

        foreach (var dataRow in dataRows)
        {
            var divsWithClasses = dataRow.ChildNodes.Where(c => c.HasClass("column")).ToList(); 
            
            if (divsWithClasses.Count == 2)
            {
                var text = divsWithClasses[0].InnerText;
                var value = divsWithClasses[1].InnerText;

                switch (text)
                {
                    case "Stellplätze:":
                    {
                        payload.Total = int.Parse(value);
                        break;
                    }
                    case "davon frei:":
                    {
                        payload.Free = int.Parse(value);
                        break;
                    }
                    case "Letzte Aktualisierung:":
                    {
                        payload.LastUpdate = DateTime.ParseExact(value, "dd.MM.yy, HH:mm", DeDe);
                        break;
                    }
                    case "GPS-Lon:":
                    {
                        if (payload.Location is null)
                        {
                            payload.Location = new Location(0, double.Parse(value, EnUs));
                        }
                        else
                        {
                            payload.Location = payload.Location with { Longitute = double.Parse(value, EnUs) };
                        }
                        break;
                    }
                    case "GPS-Lat:":
                    {
                        if (payload.Location is null)
                        {
                            payload.Location = new Location(double.Parse(value, EnUs), 0);
                        }
                        else
                        {
                            payload.Location = payload.Location with { Latitude = double.Parse(value, EnUs) };
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        var adresse = subsiteDocument.DocumentNode.SelectSingleNode("//h3[text()='Adresse']/following-sibling::div[1]/text()[1]").InnerText;
        const string pattern = @"(?<strasse>[^\d,]+)(?:\s(?<hausnummer>\d+[a-zA-Z]?))?,\s(?<plz>\d{5})\s(?<ort>[A-Za-zäöüÄÖÜß\s-]+)$";

        var regexAdresse = new Regex(pattern);
        var match = regexAdresse.Match(adresse);

        if (match.Success)
        {
            payload.AddressStreet = match.Groups["strasse"].Value.Trim();
            payload.AddressPostalCode = match.Groups["hausnummer"].Value.Trim();
            payload.AddressPostalCode = match.Groups["plz"].Value.Trim();
            payload.AddressCity = match.Groups["ort"].Value.Trim();
        }
    }
}

public class AddParkAreaPayload
{
    public int ParkAreaId { get; set; }
    public string DisplayName { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public int? Total { get; set; }
    public int? Free { get; set; }

    public string? ParkingStateName { get; set; }
    public string? ParkingStateIcon { get; set; }

    public string? OperatorWebsite { get; set; }
    public string? OperatorEmail { get; set; }

    public string? AddressStreet { get; set; }
    public string? AddressNumber { get; set; }
    public string? AddressPostalCode { get; set; }
    public string? AddressCity { get; set; }
    public Location? Location { get; set; }

    public bool ServiceTimeIsAllDayOpen { get; set; }
    public TimeOnly? ServiceTimeOpening { get; set; }
    public TimeOnly? ServiceTimeClosing { get; set; }

    public string RegionDisplayName { get; set; } = null!;
}

public record Location(double Latitude, double Longitute);