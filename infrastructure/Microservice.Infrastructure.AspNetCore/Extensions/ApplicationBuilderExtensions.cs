using Microservice.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.Infrastructure.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static void UseApplication(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        IServiceProvider serviceProvider = app.ApplicationServices;
        serviceProvider.SetApplicationBuilder(app);

        IApplication application = serviceProvider.GetRequiredService<IApplication>();
        IHostApplicationLifetime applicationLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            application.Shutdown();
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        application.SetServiceProvider(serviceProvider);
        application.Configure();
    }
}