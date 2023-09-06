namespace Ims.Application.WeatherForecasts;

public class ListWeatherForecastsQueryHandler
    : IRequestHandler<ListWeatherForecastsQuery, ApplicationResponse<ListWeatherForecastsResponse>>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public ListWeatherForecastsQueryHandler(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository ?? throw new ArgumentNullException(nameof(weatherForecastRepository));
    }

    public async Task<ApplicationResponse<ListWeatherForecastsResponse>> Handle(
        ListWeatherForecastsQuery request,
        CancellationToken cancellationToken)
    {
        var forecasts = await _weatherForecastRepository.GetWeatherForecasts();

        var forecastDtos = forecasts
            .Select(x => new WeatherForecastDto(x.Date, x.TemperatureC, x.TemperatureF, x.Summary))
            .ToList();
        var result = new ListWeatherForecastsResponse(forecastDtos);

        return ApplicationResponse.Success(result);
    }
}
