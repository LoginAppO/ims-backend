using Ims.Application.Logins;
using Ims.Application.Users;
using Ims.Application.WeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace Ims.Api.Modules;

public class LoginModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // radit ce logovanje usera, izdavanje tokena, zapisivanje u bazu da je bio login
        _ = app.MapPost("/logins/login", async ([FromServices] ISender mediator, LoginCommand loginCredentials) =>
        {
            return MapResponse.Map(await mediator.Send(loginCredentials));
        })
        .WithName("Login")
        .WithOpenApi();

        _ = app.MapGet("/logins", async ([FromServices] ISender mediator, int pageNumber, int pageSize) =>
        {
            return MapResponse.Map(await mediator.Send(new GetLoginsQuery(pageSize, pageNumber)));
        })
        .WithName("Logins")
        .WithOpenApi()
        .RequireAuthorization();

        _ = app.MapPost("/logins/filter", async ([FromServices] ISender mediator, FilterLoginsQuery filterParams) =>
        {
            return MapResponse.Map(await mediator.Send(filterParams));
        })
        .WithName("GetFilteredLogins")
        .WithOpenApi()
        .RequireAuthorization();
    }
}
