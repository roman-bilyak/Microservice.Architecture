using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.ReviewService.Reviews;

[TestFixture]
internal class MovieApplicationServiceTests : ReviewServiceTests
{
    private IMovieApplicationService _movieApplicationService;

    [SetUp]
    public void Setup()
    {
        _movieApplicationService = ServiceProvider.GetRequiredService<IMovieApplicationService>();
    }
}