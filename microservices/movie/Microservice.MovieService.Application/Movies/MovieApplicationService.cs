using MassTransit.Mediator;
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

    public async Task<MovieDto> GetMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetMovieByIdQuery>();
        var response = await client.GetResponse<MovieDto>(new GetMovieByIdQuery { Id = id }, cancellationToken);
        return response.Message;
    }

    public async Task<MovieListDto> GetMoviesAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetMoviesQuery>();
        var response = await client.GetResponse<MovieListDto>(new GetMoviesQuery { PageIndex = pageIndex, PageSize = pageSize }, cancellationToken);
        return response.Message;
    }

    public async Task<MovieDto> CreateMovieAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateMovieCommand>();
        var response = await client.GetResponse<MovieDto>(new CreateMovieCommand { Model = movie }, cancellationToken);
        return response.Message;
    }

    public async Task<MovieDto> UpdateMovieAsync([Required] Guid id, [Required] UpdateMovieDto movie, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateMovieCommand>();
        var response = await client.GetResponse<MovieDto>(new UpdateMovieCommand { Id = id, Model = movie }, cancellationToken);
        return response.Message;
    }

    public async Task DeleteMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMovieCommand { Id = id }, cancellationToken);
    }
}