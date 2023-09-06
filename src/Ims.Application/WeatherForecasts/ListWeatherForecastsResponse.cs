namespace Ims.Application.WeatherForecasts;

public record ListWeatherForecastsResponse(IReadOnlyCollection<WeatherForecastDto> Forecasts);

public record WeatherForecastDto(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);
