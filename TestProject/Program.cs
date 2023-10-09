using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestProject;

public class Program
{
    public Program()
    {
        var builder = Host.CreateDefaultBuilder();

        var host = builder.Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;

            var config = services.GetRequiredService<IConfiguration>();

            var atcomUrl = config["AtcomUrl"];
        }
    }
}