using Microservice.TestService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

DbContextOptionsBuilder<TestServiceDbContext> optionsBuilder = new();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestServiceDb"));

using TestServiceDbContext dbContext = new(optionsBuilder.Options);
dbContext.Database.Migrate();

Console.WriteLine("Migration completed!");
