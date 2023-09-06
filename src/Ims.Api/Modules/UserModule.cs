using Ims.Application.Users;
using Ims.SharedCore;
using Microsoft.AspNetCore.Mvc;

namespace Ims.Api.Modules;

public class UserModule : IRouteModule
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/users", async ([FromServices] ISender mediator, int pageNumber, int pageSize) =>
        {
            return MapResponse.Map(await mediator.Send(new GetUsersQuery(pageSize, pageNumber)));
        })
        .WithName("Users")
        .WithOpenApi()
        .RequireAuthorization();

        _ = app.MapPost("/users/filter", async ([FromServices] ISender mediator, FilterUsersQuery filterParams) =>
        {
            return MapResponse.Map(await mediator.Send(filterParams));
        })
        .WithName("GetFilteredUsers")
        .WithOpenApi()
        .RequireAuthorization();

        _ = app.MapPost("/users", async ([FromServices] ISender mediator, AddUserCommand user) =>
        {
            return MapResponse.Map(await mediator.Send(user));
        })
        .WithName("AddUser")
        .WithOpenApi()
        .RequireAuthorization();
    }
}
