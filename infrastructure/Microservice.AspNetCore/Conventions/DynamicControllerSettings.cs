using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

internal class DynamicControllerSettings
{
    public Func<Type, bool> TypePredicate { get; private set; }

    public string RootPath { get; set; }

    public DynamicControllerSettings(Func<Type, bool> typePredicate, string rootPath)
    {
        ArgumentNullException.ThrowIfNull(typePredicate);

        TypePredicate = typePredicate;
        RootPath = rootPath;
    }
}