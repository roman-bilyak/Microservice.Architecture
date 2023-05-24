using Microservice.PaymentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

DbContextOptionsBuilder<PaymentServiceDbContext> optionsBuilder = new();
optionsBuilder.UseSqlServer(configuration.GetConnectionString(nameof(PaymentServiceDbContext)));

using PaymentServiceDbContext dbContext = new(optionsBuilder.Options);
dbContext.Database.Migrate();

Console.WriteLine("Migration completed!");