using System.Runtime.Serialization;

namespace Microservice.Core;

public class EntityNotFoundException : BaseException
{
    public Type EntityType { get; protected set; }

    public object Id { get; protected set; }

    public EntityNotFoundException(Type entityType, object id)
        : this(entityType, id, null)
    {

    }

    public EntityNotFoundException(Type entityType, object id, Exception? innerException)
        : base($"Entity (type = '{entityType.FullName}', id = '{id}') not found", innerException)
    {
        EntityType = entityType;
        Id = id;
    }
}