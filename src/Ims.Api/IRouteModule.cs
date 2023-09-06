using System.Reflection;
using Ims.SharedCore;

namespace Ims.Api;

public interface IRouteModule
{
    void DefineEndpoints(IEndpointRouteBuilder app);
}

public static class RouteModuleDependencyInjection
{
    public static void AddRouteModules(this IServiceCollection services, params Type[] scanMarkers)
    {
        var modules = new List<IRouteModule>();

        modules.AddRange(
            Assembly.GetExecutingAssembly().ExportedTypes
                .Where(type => typeof(IRouteModule).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .Select(Activator.CreateInstance).Cast<IRouteModule>());

        foreach (var scanMarker in scanMarkers)
        {
            modules.AddRange(
                scanMarker.Assembly.ExportedTypes
                    .Where(type => typeof(IRouteModule).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    .Select(Activator.CreateInstance).Cast<IRouteModule>());
        }

        _ = services.AddSingleton(modules as IReadOnlyCollection<IRouteModule>);
    }

    public static void UseRouteModules(this WebApplication app)
    {
        var modules = app.Services.GetRequiredService<IReadOnlyCollection<IRouteModule>>();

        foreach (var module in modules)
            module.DefineEndpoints(app);
    }
}

public static class MapResponse
{
    public static IResult Map(ApplicationResponse applicationResponse)
    {
        return applicationResponse.StatusCode switch
        {
            "OK" => Results.Ok(applicationResponse),
            "Fail" => Results.BadRequest(applicationResponse),
            "BadRequest" => Results.BadRequest(applicationResponse),
            "NotFound" => Results.NotFound(applicationResponse),
            _ => Results.Ok(applicationResponse),
        };
    }
}
