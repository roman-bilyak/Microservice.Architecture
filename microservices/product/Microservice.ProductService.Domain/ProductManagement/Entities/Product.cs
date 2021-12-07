using Microservice.Infrastructure.Database;

namespace Microservice.ProductService.Domain;

public class Product : Entity<Guid>, IAggregateRoot
{
    public string Name { get; set; }
}