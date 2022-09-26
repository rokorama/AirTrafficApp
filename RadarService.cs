using Microsoft.Extensions.Hosting;

public class RadarService : IHostedService
{
    private Map? _map;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public RadarService(IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(StartApplication);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void StartApplication()
    {
        Console.WriteLine("To start, please enter a filepath to a .map file:");
        bool promptForFilepath = true;

        while (promptForFilepath is true)
        {
            var filepathInput = Console.ReadLine();
            try
            {
                _map = new Map(filepathInput!);
                promptForFilepath = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine("Map loaded successfully. Commencing listening for input.");
    }

    public void ProcessInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            MapService.UpdateMap(_map!, input!);
        }         
    }
}