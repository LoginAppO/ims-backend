using Ims.Application.WeatherForecasts;
using Ims.SharedCore;

namespace Ims.IntegrationTests;

public class WeatherForecastTestsTests : IClassFixture<ImsWebApplicationFactory<Program>>
{
    private readonly ImsWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public WeatherForecastTestsTests(ImsWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task List_weather_forecasts_returns_non_empty_list_of_forecasts()
    {
        var response = await _client.GetAsync("weatherforecast")
            .ConfigureAwait(false);

        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);

        var result = await response.Content.ReadFromJsonAsync<ApplicationResponse<ListWeatherForecastsResponse>>()
            .ConfigureAwait(false);

        Assert.NotNull(result);
        Assert.True(result!.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
    }
}
