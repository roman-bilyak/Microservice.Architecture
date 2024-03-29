﻿namespace Microservice.MovieService.Movies;

public interface IMovieManager
{
    Task<Movie?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Movie>> ListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);

    Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken);

    Task<Movie> UpdateAsync(Movie movie, CancellationToken cancellationToken);

    Task DeleteAsync(Movie movie, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}