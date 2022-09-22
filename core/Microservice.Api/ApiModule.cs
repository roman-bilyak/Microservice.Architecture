using Microservice.AspNetCore;
using Microservice.AspNetCore.Authentication;
using Microservice.AspNetCore.Authorization;
using Microservice.AspNetCore.Swagger;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;

namespace Microservice.Api
{
    [DependsOn(typeof(AuthenticationModule))]
    [DependsOn(typeof(AuthorizationModule))]
    [DependsOn(typeof(SwaggerModule))]
    public sealed class ApiModule : StartupModule
    {
        public override void PreConfigure(IServiceProvider serviceProvider)
        {
            base.PreConfigure(serviceProvider);

            IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
            app.UseRouting();
        }

        public override void PostConfigure(IServiceProvider serviceProvider)
        {
            base.PostConfigure(serviceProvider);

            IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
            app.UseEndpoints(x => x.MapDefaultControllerRoute());
        }
    }
}