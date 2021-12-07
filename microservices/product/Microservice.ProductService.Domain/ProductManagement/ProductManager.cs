using Microservice.Infrastructure.Database;

namespace Microservice.ProductService.Domain;

internal class ProductManager : IProductManager
{
    private readonly IRepository<Product> _productRepository;

    public ProductManager(IRepository<Product> productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository);

        _productRepository = productRepository;
    }

    public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Product>> ListAsync(CancellationToken cancellationToken)
    {
        return await _productRepository.ListAsync(cancellationToken);
    }

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
    {
        return await _productRepository.AddAsync(product, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        return await _productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(product, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _productRepository.SaveChangesAsync(cancellationToken);
    }
}