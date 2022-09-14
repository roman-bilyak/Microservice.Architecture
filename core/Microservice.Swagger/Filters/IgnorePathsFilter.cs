using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microservice.Swagger;

internal class IgnorePathsFilter : IDocumentFilter
{
    private readonly SwaggerOptions _options;

    public IgnorePathsFilter(IOptions<SwaggerOptions> options)
    {
        _options = options.Value;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (_options.IgnorePaths is null)
        {
            return;
        }

        foreach (string ignorePath in _options.IgnorePaths)
        {
            swaggerDoc.Paths.Remove(ignorePath);
        }
    }
}