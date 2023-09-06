using Ims.Application.WeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace Ims.Api.Modules;

public class WeatherForecastModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/weatherforecast", async ([FromServices] ISender mediator) =>
        {
            var result = await mediator.Send(new ListWeatherForecastsQuery());
            return result;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
