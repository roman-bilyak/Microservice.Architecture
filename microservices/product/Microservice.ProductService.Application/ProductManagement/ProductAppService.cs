using Microservice.ProductService.Domain;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ProductService.Application;

internal class ProductAppService : IProductAppService
{
    private readonly IProductManager _productManager;

    public ProductAppService(IProductManager productManager)
    {
        _productManager = productManager;
    }

    public async Task<ProductDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        Product product = await _productManager.GetByIdAsync(id, cancellationToken);
        if (product == null)
        {
            throw new Exception($"Product (id = '{id}') not found");
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name
        };
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<ProductDto> result = new List<ProductDto>();
        List<Product> products = await _productManager.ListAsync(cancellationToken);

        foreach (Product product in products)
        {
            result.Add(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
            });
        }

        return result;
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto product, CancellationToken cancellationToken)
    {
        Product entity = new Product
        {
            Name = product.Name
        };

        await _productManager.AddAsync(entity, cancellationToken);
        await _productManager.SaveChangesAsync(cancellationToken);

        return new ProductDto
        {
            Id = entity.Id,
            Name = product.Name
        };
    }

    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto product, CancellationToken cancellationToken)
    {
        Product entity = await _productManager.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Product (id = '{id}') not found");
        }

        entity.Name = product.Name;

        await _productManager.UpdateAsync(entity, cancellationToken);
        await _productManager.SaveChangesAsync(cancellationToken);

        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Product entity = await _productManager.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Product (id = '{id}') not found");
        }

        await _productManager.DeleteAsync(entity, cancellationToken);
        await _productManager.SaveChangesAsync(cancellationToken);
    }
}