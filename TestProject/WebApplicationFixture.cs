using Integrationtest;
using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.Server;
using XUnitRunerOverload;

[assembly: TestFramework("XUnitRunerOverload.FrameworkWithAssemblyFixture", "XUnitRunerOverload")]
[assembly: GlobalFixture(typeof(WebApplicationFixture))]

namespace Integrationtest;

public class WebApplicationFixture : IDisposable
{
    private WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();

    public HttpClient Client;

    public WebApplicationFixture()
    {
        var server = WireMockServer.Start(5001);

        Client = factory.CreateClient();

    }
    
    public void Dispose()
    {
        factory.Dispose();
    }
}