namespace Microservice.Database;

public abstract class Entity : Entity<Guid>
{
}

public abstract class Entity<T> where T : notnull
{
    public T Id { get; set; }
}