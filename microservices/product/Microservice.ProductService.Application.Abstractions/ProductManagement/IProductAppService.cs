namespace Microservice.ProductService.Application;

public interface IProductAppService
{
    public Task<ProductDto> GetAsync(Guid id);

    public Task<List<ProductDto>> GetAllAsync();

    public Task<ProductDto> CreateAsync(CreateProductDto product);

    public Task<ProductDto> UpdateAsync(UpdateProductDto product);

    public Task DeleteAsync(Guid id);

}