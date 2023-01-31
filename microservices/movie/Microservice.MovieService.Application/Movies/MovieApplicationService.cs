﻿using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

[Authorize]
internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IMediator _mediator;

    public MovieApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<MovieListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetMoviesQuery>();
        var response = await client.GetResponse<MovieListDto>(new GetMoviesQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<MovieDto> GetAsync([Required] Guid movieId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetMovieByIdQuery>();
        var response = await client.GetResponse<MovieDto>(new GetMovieByIdQuery(movieId), cancellationToken);
        return response.Message;
    }

    public async Task<MovieDto> CreateAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateMovieCommand>();
        var response = await client.GetResponse<MovieDto>(new CreateMovieCommand(movie), cancellationToken);
        return response.Message;
    }

    public async Task<MovieDto> UpdateAsync([Required] Guid movieId, [Required] UpdateMovieDto movie, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateMovieCommand>();
        var response = await client.GetResponse<MovieDto>(new UpdateMovieCommand(movieId, movie), cancellationToken);
        return response.Message;
    }

    public async Task DeleteAsync([Required] Guid movieId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMovieCommand(movieId), cancellationToken);
    }
}