using Microsoft.Extensions.DependencyInjection;

namespace DvbCs;

public static class ServiceRegistrations
{
    public static void AddDvbServices(this IServiceCollection services)
    {
        services.AddHttpClient(HttpClientNames.Dvb.DepartureMonitior, client =>
        {
            client.BaseAddress = new Uri("https://webapi.vvo-online.de/dm");
        });
        services.AddSingleton<DepartureMonitorService>();
    }
}