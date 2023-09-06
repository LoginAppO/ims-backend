using Ims.Domain;

namespace Ims.Infrastructure;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    public Task<IReadOnlyCollection<WeatherForecast>> GetWeatherForecasts()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]))
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<WeatherForecast>>(forecast);
    }
}
