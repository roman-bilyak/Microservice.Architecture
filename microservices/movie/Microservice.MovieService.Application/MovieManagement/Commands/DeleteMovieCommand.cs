using MediatR;
using Microservice.Core.CQRS.Commands;

namespace Microservice.MovieService.MovieManagement.Commands
{
    internal class DeleteMovieCommand : DeleteCommand<Guid>
    {
        internal class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
        {
            private readonly IMovieManager _movieManager;

            public DeleteMovieCommandHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager;
            }

            public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
            {
                Movie entity = await _movieManager.GetByIdAsync(request.Id, cancellationToken);
                if (entity == null)
                {
                    throw new Exception($"Movie (id = '{request.Id}') not found");
                }

                await _movieManager.DeleteAsync(entity, cancellationToken);
                await _movieManager.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}