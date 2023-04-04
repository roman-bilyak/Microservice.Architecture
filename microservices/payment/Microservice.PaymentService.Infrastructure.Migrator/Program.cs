using Microservice.PaymentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

DbContextOptionsBuilder<PaymentServiceDbContext> optionsBuilder = new();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("PaymentServiceDb"));

using PaymentServiceDbContext dbContext = new(optionsBuilder.Options);
dbContext.Database.Migrate();

Console.WriteLine("Migration completed!");