using FluentAssertions;
using HtmlAgilityPack;

namespace ParkplatzDresden.ScraperLib.Tests;

public class ParkAreaTests
{
    private readonly Scraper _scraper = new();

    private readonly Func<Task<HtmlDocument>> _source = () =>
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.Load(File.OpenRead("TestSites/StartPage.html"));
        return Task.FromResult(htmlDocument);
    };

    private readonly Func<string, Task<HtmlDocument>> _sourceSubsite = _ =>
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.Load(File.OpenRead("TestSites/Einzelseite_Altmarkt.html"));
        return Task.FromResult(htmlDocument);
    };

    private Task<List<ParkArea>> _scrape() => _scraper.ScrapeAsync(_source, _sourceSubsite);

    [Fact]
    public async Task Expect_52_ParkAreas()
    {
        var result = await _scrape();
        result.Should().HaveCount(52);
    }

    [Fact]
    public async Task Expect_ParkArea_Altmarkt_should_have_id_431()
    {
        var result = await _scrape();
        var altmarkt = result.Single(r => r.DisplayName == "Altmarkt");
        altmarkt.Id.Should().Be(431);
    }
    
    [Fact]
    public async Task Expect_ParkArea_Altmarkt_has_400_Stellplaetze()
    {
        var result = await _scrape();
        var altmarkt = result.Single(r => r.DisplayName == "Altmarkt");
        altmarkt.ParkingSlot.Should().NotBeNull();
        altmarkt.ParkingSlot.Total.Should().Be(400);
    }
    
    [Fact]
    public async Task Expect_ParkArea_Altmarkt_has_373_free_spaces()
    {
        var result = await _scrape();
        var altmarkt = result.Single(r => r.DisplayName == "Altmarkt");
        altmarkt.ParkingSlot.Should().NotBeNull();
        altmarkt.ParkingSlot.Free.Should().Be(373);
    }
}