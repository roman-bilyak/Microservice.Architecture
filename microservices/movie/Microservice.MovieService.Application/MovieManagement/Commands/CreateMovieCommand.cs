using Microservice.Core.CQRS.Commands;

namespace Microservice.MovieService.MovieManagement.Commands
{
    internal class CreateMovieCommand : CreateCommand<CreateMovieDto, MovieDto>
    {
        internal class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand, MovieDto>
        {
            private readonly IMovieManager _movieManager;

            public CreateMovieCommandHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager;
            }

            public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
            {
                Movie entity = new Movie
                {
                    Title = request.Model.Title
                };

                entity = await _movieManager.AddAsync(entity, cancellationToken);
                await _movieManager.SaveChangesAsync(cancellationToken);

                return new MovieDto
                {
                    Id = entity.Id,
                    Title = request.Model.Title
                };
            }
        }
    }
}