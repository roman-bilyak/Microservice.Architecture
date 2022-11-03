﻿namespace Microservice.CQRS;

public abstract class DeleteCommand<TId> : Command
    where TId : struct
{
    public TId Id { get; protected set; }

    protected DeleteCommand(TId id)
    {
        Id = id;
    }
}