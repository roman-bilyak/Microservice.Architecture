using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

public class DynamicControllerOptions
{
    private readonly List<DynamicControllerSettings> _settings = new List<DynamicControllerSettings>();

    public IReadOnlyList<Assembly> Assemblies => _settings.Select(x => x.Assembly).ToList();

    public void AddSettings(Assembly assembly, Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(typePredicate);

        _settings.Add(new DynamicControllerSettings(assembly, typePredicate));
    }

    public bool IsController(Type type)
    {
        return _settings.Any(x => x.Assembly == type.Assembly && x.TypePredicate(type));
    }
}