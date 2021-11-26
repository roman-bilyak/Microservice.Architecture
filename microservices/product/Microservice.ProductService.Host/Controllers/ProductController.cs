using Microservice.ProductService.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.ProductService.Host.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2"
            },
        };
    }
}