using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.MovieService.Movies;

[TestFixture]
internal class MovieApplicationServiceTests : MovieServiceTests
{
    private IMovieApplicationService _movieApplicationService;

    [SetUp]
    public void Setup()
    {
        _movieApplicationService = ServiceProvider.GetRequiredService<IMovieApplicationService>();
    }

    [Test]
    public async Task GetList_WhenNoMovies_ReturnsEmptyList()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 5;

        // Act
        MovieListDto movieListDto = await _movieApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(movieListDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieListDto.Items, Is.Empty);
            Assert.That(movieListDto.TotalCount, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task GetList_WhenLessMoviesThanPageSize_ReturnsCorrectNumberOfMovies()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 5;

        for (int i = 0; i < 3; i++)
        {
            CreateMovieDto createMovieDto = new()
            {
                Title = $"Movie {Guid.NewGuid()}"
            };
            await _movieApplicationService.CreateAsync(createMovieDto);
        }

        // Act
        MovieListDto movieListDto = await _movieApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(movieListDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieListDto.Items, Has.Count.EqualTo(3));
            Assert.That(movieListDto.TotalCount, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task GetList_WhenMoreMoviesThanPageSize_ReturnsCorrectPageOfMovies()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 5;
        for (int i = 0; i < 7; i++)
        {
            CreateMovieDto createMovieDto = new()
            {
                Title = $"Movie {Guid.NewGuid()}"
            };
            await _movieApplicationService.CreateAsync(createMovieDto);
        }

        // Act
        MovieListDto movieListDto = await _movieApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(movieListDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieListDto.Items, Has.Count.EqualTo(5));
            Assert.That(movieListDto.TotalCount, Is.EqualTo(7));
        });
    }

    [Test]
    public async Task GetList_WhenPageIndexGreaterThanTotalPages_ReturnsEmptyList()
    {
        // Arrange
        int pageIndex = 10;
        int pageSize = 10;

        for(int i = 0; i < 3; i++)
        {
            CreateMovieDto createMovieDto = new()
            {
                Title = $"Movie {Guid.NewGuid()}"
            };
            await _movieApplicationService.CreateAsync(createMovieDto);
        }

        // Act
        MovieListDto movieListDto = await _movieApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(movieListDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieListDto.Items, Is.Empty);
            Assert.That(movieListDto.TotalCount, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task GetList_WhenPageSizeIsZero_ReturnsEmptyList()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 0;

        for (int i = 0; i < 2; i++)
        {
            CreateMovieDto createMovieDto = new()
            {
                Title = $"Movie {Guid.NewGuid()}"
            };
            await _movieApplicationService.CreateAsync(createMovieDto);
        }

        // Act
        MovieListDto movieListDto = await _movieApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(movieListDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieListDto.Items, Is.Empty);
            Assert.That(movieListDto.TotalCount, Is.EqualTo(2));
        });
    }
}