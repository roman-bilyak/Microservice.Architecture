﻿using Microservice.Database;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.MovieService.Movies;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; protected internal set; }

    protected Movie()
    {

    }

    public Movie(Guid id, string title)
        : base(id)
    {
        SetTitle(title);
    }

    [MemberNotNull(nameof(Title))]
    public void SetTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));

        Title = title;
    }
}