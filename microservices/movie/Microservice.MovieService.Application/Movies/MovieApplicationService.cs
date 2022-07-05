using MediatR;
using Microservice.Application.Services;
using Microservice.MovieService.Movies.Commands;
using Microservice.MovieService.Movies.Queries;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IMediator _mediator;

    public MovieApplicationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<MovieDto> GetMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        GetMovieByIdQuery query = new GetMovieByIdQuery { Id = id };
        return await _mediator.Send(query, cancellationToken);
    }

    public async Task<MovieListDto> GetMoviesAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        GetMoviesQuery query = new GetMoviesQuery { PageIndex = pageIndex, PageSize = pageSize };
        return await _mediator.Send(query, cancellationToken);
    }

    public async Task<MovieDto> CreateMovieAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken)
    {
        CreateMovieCommand command = new CreateMovieCommand { Model = movie };
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<MovieDto> UpdateMovieAsync([Required] Guid id, [Required] UpdateMovieDto movie, CancellationToken cancellationToken)
    {
        UpdateMovieCommand command = new UpdateMovieCommand { Id = id, Model = movie };
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task DeleteMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        DeleteMovieCommand command = new DeleteMovieCommand { Id = id };
        await _mediator.Send(command, cancellationToken);
    }
}