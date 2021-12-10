﻿using Microservice.Infrastructure.Database;

namespace Microservice.ReviewService.Domain.Reviews;

public class Review : Entity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }

    public Guid MovieId { get; set; }

    public string Text { get; set; }

    public RatingEnum Rating { get; set; }
}