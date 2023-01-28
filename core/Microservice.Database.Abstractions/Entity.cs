namespace Microservice.Database;

public abstract class Entity
{
}

public abstract class Entity<T>
    where T : struct
{
    public T Id { get; protected set; }

    protected Entity()
    {

    }

    protected Entity(T id)
    {
        Id = id;
    }
}