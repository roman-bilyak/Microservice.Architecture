namespace Microservice.Infrastructure.Database;

public class Entity : Entity<Guid>
{
}

public class Entity<T> where T : notnull
{
    public T Id { get; set; }
}