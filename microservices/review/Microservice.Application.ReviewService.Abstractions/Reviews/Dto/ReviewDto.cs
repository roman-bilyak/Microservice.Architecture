﻿namespace Microservice.ReviewService.Reviews;

public record ReviewDto
{
    public Guid Id { get; init; }

    public Guid MovieId { get; init; }

    public Guid UserId { get; init; }

    public string Comment { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}