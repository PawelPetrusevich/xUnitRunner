using Microsoft.Extensions.Hosting;
using WireMock.Server;

namespace HostedService;

public class WireMockRunner : IHostedService
{
    private WireMockServer server;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        server = WireMockServer.Start(5002);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        server.Stop();
        return Task.CompletedTask;
    }
}