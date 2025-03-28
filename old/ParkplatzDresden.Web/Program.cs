using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StrawberryShake;

namespace ParkplatzDresden.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services
            .AddParkplatzDresdenClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://apiservice/graphql"))
            .ConfigureWebSocketClient(client => client.Uri = new Uri("ws://apiservice/graphql"));
        
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}