using Ims.Application.WeatherForecasts;
using Ims.Infrastructure;

namespace Ims.UnitTests;

public class WeatherForecastTests
{
    [Fact]
    public async Task List_weather_forecasts_returns_non_empty_list_of_forecasts()
    {
        var handler = new ListWeatherForecastsQueryHandler(new WeatherForecastRepository());
        var query = new ListWeatherForecastsQuery();

        var result = await handler.Handle(query, CancellationToken.None)
            .ConfigureAwait(false);

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
    }
}
