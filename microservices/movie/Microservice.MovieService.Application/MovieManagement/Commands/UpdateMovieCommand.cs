using Microservice.Core.CQRS.Commands;

namespace Microservice.MovieService.MovieManagement.Commands
{
    internal class UpdateMovieCommand : UpdateCommand<Guid, UpdateMovieDto, MovieDto>
    {
        internal class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand, MovieDto>
        {
            private readonly IMovieManager _movieManager;

            public UpdateMovieCommandHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager;
            }

            public async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
            {
                Movie entity = await _movieManager.GetByIdAsync(request.Id, cancellationToken);
                if (entity == null)
                {
                    throw new Exception($"Movie (id = '{request.Id}') not found");
                }

                entity.Title = request.Model.Title;

                entity = await _movieManager.UpdateAsync(entity, cancellationToken);
                await _movieManager.SaveChangesAsync(cancellationToken);

                return new MovieDto
                {
                    Id = entity.Id,
                    Title = entity.Title
                };
            }
        }
    }
}