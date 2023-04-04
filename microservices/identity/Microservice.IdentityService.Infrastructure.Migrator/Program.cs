using Microservice.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

DbContextOptionsBuilder<IdentityServiceDbContext> optionsBuilder = new();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityServiceDb"));

using IdentityServiceDbContext dbContext = new(optionsBuilder.Options);
dbContext.Database.Migrate();

Console.WriteLine("Migration completed!");