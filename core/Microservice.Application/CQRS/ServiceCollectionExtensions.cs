using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Application.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services
                .AddMediator(x =>
                {
                    x.AddConsumers(assemblies);
                });
        }
    }
}