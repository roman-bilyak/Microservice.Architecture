namespace Microservice.Database;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}