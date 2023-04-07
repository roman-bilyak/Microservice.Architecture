using MassTransit;
using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

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

    public async Task<MovieListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetMoviesQuery(pageIndex, pageSize), cancellationToken);
    }

    public async Task<MovieDto> GetAsync(Guid movieId, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetMovieByIdQuery(movieId), cancellationToken);
    }

    public async Task<MovieDto> CreateAsync(CreateMovieDto movie, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new CreateMovieCommand(movie), cancellationToken);
    }

    public async Task<MovieDto> UpdateAsync(Guid movieId, UpdateMovieDto movie, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new UpdateMovieCommand(movieId, movie), cancellationToken);
    }

    public async Task DeleteAsync(Guid movieId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new DeleteMovieCommand(movieId), cancellationToken);
    }
}