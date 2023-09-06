namespace Ims.Domain;

public interface IWeatherForecastRepository
{
    Task<IReadOnlyCollection<WeatherForecast>> GetWeatherForecasts();
}
