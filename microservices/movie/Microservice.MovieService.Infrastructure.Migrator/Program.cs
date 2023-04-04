using Microservice.MovieService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

DbContextOptionsBuilder<MovieServiceDbContext> optionsBuilder = new();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("MovieServiceDb"));

using MovieServiceDbContext dbContext = new(optionsBuilder.Options);
dbContext.Database.Migrate();

Console.WriteLine("Migration completed!");