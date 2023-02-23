namespace Microservice.CQRS;

public class Unit
{
    private static readonly Unit _value = new();

    public static ref readonly Unit Value => ref _value;
}