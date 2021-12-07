namespace Microservice.ProductService.Domain;

public interface IProductManager
{
    Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Product>> ListAsync(CancellationToken cancellationToken);

    Task<Product> AddAsync(Product product, CancellationToken cancellationToken);

    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);

    Task DeleteAsync(Product product, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}