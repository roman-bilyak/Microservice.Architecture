namespace Microservice.AspNetCore.Conventions;

internal class DynamicControllerSettings
{
    public Func<Type, bool> TypePredicate { get; private set; }

    public DynamicControllerSettings(Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(typePredicate);

        TypePredicate = typePredicate;
    }
}