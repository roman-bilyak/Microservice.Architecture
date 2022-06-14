﻿using Microservice.Core.Database;

namespace Microservice.MovieService.MovieManagement;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; set; }
}