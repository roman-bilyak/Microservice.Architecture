using System.Reflection;

namespace Microservice.AspNetCore;

public class DynamicControllerOptions
{
    private readonly IDictionary<Assembly, DynamicControllerSettings> _settings = new Dictionary<Assembly, DynamicControllerSettings>();

    public IReadOnlyList<Assembly> Assemblies => _settings.Select(x => x.Key).ToList();

    public void AddSettings(Assembly assembly, Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(typePredicate);

        if (_settings.ContainsKey(assembly))
        {
            throw new ArgumentException($"Settings for specified assembly '{assembly.FullName}' already exist", nameof(assembly));
        }
        _settings.Add(assembly, new DynamicControllerSettings(typePredicate));
    }

    public bool IsController(Type type)
    {
        return _settings.Any(x => x.Key == type.Assembly && x.Value.TypePredicate(type));
    }
}