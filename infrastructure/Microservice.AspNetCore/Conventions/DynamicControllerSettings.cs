using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

internal class DynamicControllerSettings
{
    public Assembly Assembly { get; private set; }

    public Func<Type, bool> TypePredicate { get; private set; }

    public DynamicControllerSettings(Assembly assembly, Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(typePredicate);

        Assembly = assembly;
        TypePredicate = typePredicate;
    }
}