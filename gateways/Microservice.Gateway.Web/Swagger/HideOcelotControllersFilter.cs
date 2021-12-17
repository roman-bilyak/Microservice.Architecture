using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microservice.Gateway.Swagger;

internal class HideOcelotControllersFilter : IDocumentFilter
{
    private static readonly string[] _ignoredPaths =
        {
            "/configuration/FileConfigurationController/Get",
            "/configuration/FileConfigurationController/Post",
            "/outputcache/{region}",
            "/outputcache/OutputCacheController/Delete"
        };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var ignorePath in _ignoredPaths)
        {
            swaggerDoc.Paths.Remove(ignorePath);
        }
    }
}