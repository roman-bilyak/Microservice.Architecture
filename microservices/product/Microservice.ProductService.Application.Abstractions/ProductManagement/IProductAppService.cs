namespace Microservice.ProductService.Application;

public interface IProductAppService
{
    public Task<ProductDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<ProductDto> CreateAsync(CreateProductDto product, CancellationToken cancellationToken = default);

    public Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto product, CancellationToken cancellationToken = default);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

}