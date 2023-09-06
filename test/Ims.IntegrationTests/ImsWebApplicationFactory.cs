using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Ims.IntegrationTests;

public class ImsWebApplicationFactory<TStartup> :
    WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();
        });
    }
}
