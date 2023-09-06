using System.Reflection;
using Ims.Application.WeatherForecasts;
using Ims.Domain;
using Ims.SharedCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ims.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionHandlerBehavior<,>));
        _ = services.AddTransient<ISystemClock, SystemClock>();

        _ = services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ListWeatherForecastsQuery))!));

        _ = services.AddEntityFrameworkSqlServer().AddDbContext<ImsDbContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Ims.Infrastructure")));

        _ = services.AddTransient(typeof(IPaginationRepository<>), typeof(PaginationRepository<>));
        _ = services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
        _ = services.AddTransient<IUserRepository, UserRepository>();
        _ = services.AddTransient<ILoginRepository, LoginRepository>();

        return services;
    }
}
