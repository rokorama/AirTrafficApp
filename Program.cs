using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                       .ConfigureServices(services => { services.AddTransient<RadarService>(); })
                       .Build();

        var radar = host.Services.GetRequiredService<RadarService>();
        radar.StartApplication();
        radar.ProcessInput();
    }
}