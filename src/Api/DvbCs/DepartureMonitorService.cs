using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DvbCs;

public interface IDepartureMonitorService
{
    Task<DepartureMonitor?> GetDepartureMonitorForStopIdAsync(string stopId, GetDepartureMonitorForStopIdOptions options, CancellationToken cancellationToken = default);
}

public class DepartureMonitorService(IHttpClientFactory httpClientFactory) : IDepartureMonitorService
{
    public async Task<DepartureMonitor?> GetDepartureMonitorForStopIdAsync(string stopId, GetDepartureMonitorForStopIdOptions options, CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientNames.Dvb.DepartureMonitior);

        var parameters = $"?stopId={stopId}";
        if (options.Limit is not null && options.Limit > 0)
        {
            parameters += $"&limit={options.Limit}";
        }
        
        var respone = await httpClient.GetAsync(parameters, cancellationToken);

        if (!respone.IsSuccessStatusCode)
        {
            return null;
        }

        var stream = await respone.Content.ReadAsStreamAsync(cancellationToken);
        
        var result = await JsonSerializer.DeserializeAsync<DepartureMonitor>(stream, JsonSerializerOptions.Default, cancellationToken);

        return result is not null
            ? result.Status.Code == "Ok"
                ? result
                : null
            : null;
    }
}

public class GetDepartureMonitorForStopIdOptions
{
    public int? Limit { get; set; }
}

internal class MicrosoftDateFormatConverter : JsonConverter<DateTime>
{
    private static readonly Regex RegexDate = new Regex(@"\/Date\((\d+)([+-]\d{4})?\)\/");

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var match = RegexDate.Match(reader.GetString() ?? string.Empty);

        if (!match.Success)
        {
            throw new JsonException("Invalid Microsoft date format");
        }

        var milliseconds = long.Parse(match.Groups[1].Value);

        var dateTimeUtc = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;

        if (!match.Groups[2].Success)
        {
            return dateTimeUtc;
        }

        var offsetStr = match.Groups[2].Value;
        var offsetStrWithDoublepoint = offsetStr.Insert(3, ":")[1..];
        var offset = TimeSpan.ParseExact(offsetStrWithDoublepoint, "hh\\:mm", null);
        return dateTimeUtc + offset;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var milliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
        writer.WriteStringValue($"/Date({milliseconds}+0000)/");
    }
}

public class DepartureMonitor
{
    public string Name { get; set; } = null!;
    public string? Place { get; set; }
    
    [JsonConverter(typeof(MicrosoftDateFormatConverter))]
    public DateTime ExpirationTime { get; set; }
    public List<Departure> Departures { get; set; } = [];
    public Status Status { get; set; }
}

public class Status
{
    public string Code { get; set; } = null!;
}

public class Departure
{
    public string? LineName { get; set; }
    public string? Direction { get; set; }
    public Platform? Platform { get; set; }
    public string Mot { get; set; }
    
    [JsonConverter(typeof(MicrosoftDateFormatConverter))]
    public DateTime RealTime { get; set; }
    
    [JsonConverter(typeof(MicrosoftDateFormatConverter))]
    public DateTime ScheduledTime { get; set; }

    public string? State { get; set; }
}

public class Platform
{
    public string Name { get; set; }
    public string Type { get; set; }
}