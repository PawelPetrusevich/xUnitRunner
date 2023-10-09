using Integrationtest;

namespace TestProject;

public class UnitTest1
{
    private readonly WebApplicationFixture fixture;

    public UnitTest1(WebApplicationFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Test1()
    {
        var result = await fixture.Client.GetStringAsync("weather/");
    }
}