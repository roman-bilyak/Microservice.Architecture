using Microservice.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Microservice.MovieService.Movies;

[TestFixture]
internal class MovieApplicationServiceTests : MovieServiceTests
{
    private IMovieApplicationService _movieApplicationService;

    [SetUp]
    public void Setup()
    {
        _movieApplicationService = GetRequiredService<IMovieApplicationService>();
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

    [Test]
    public void GetList_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 10;
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.GetListAsync(pageIndex, pageSize, cancellationToken));
    }

    [Test]
    public async Task Get_WithExistingMovieId_ReturnsMovie()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);

        // Act
        MovieDto result = await _movieApplicationService.GetAsync(movieDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(createMovieDto.Title));
    }

    [Test]
    public void Get_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.Empty;

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.GetAsync(movieId));
    }

    [Test]
    public void Get_WithCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.GetAsync(movieId, cancellationToken));
    }

    [Test]
    public async Task Create_WithValidData_CreatesAndReturnsCreatedMovie()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };

        // Act
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);
        MovieDto getMovieDto = await _movieApplicationService.GetAsync(movieDto.Id);

        // Assert
        Assert.That(movieDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(movieDto.Title, Is.EqualTo(createMovieDto.Title));
            Assert.That(getMovieDto, Is.Not.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(getMovieDto.Id, Is.EqualTo(movieDto.Id));
            Assert.That(getMovieDto.Title, Is.EqualTo(createMovieDto.Title));
        });
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Create_WithEmptyTitle_ThrowsDataValidationException(string invalidTitle)
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = invalidTitle
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.CreateAsync(createMovieDto));
    }

    [Test]
    public void Create_WithMaxTitleLength_ThrowsDataValidationException()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = new string('x', 101)
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.CreateAsync(createMovieDto));
    }

    [Test]
    public void Create_WithCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.CreateAsync(createMovieDto, cancellationToken));
    }

    [Test]
    public async Task Update_WithExistingMovieId_UpdatesAndReturnsUpdatedMovie()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);
        UpdateMovieDto updateMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };

        // Act
        MovieDto result = await _movieApplicationService.UpdateAsync(movieDto.Id, updateMovieDto);
        MovieDto getMovieDto = await _movieApplicationService.GetAsync(movieDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Title, Is.EqualTo(updateMovieDto.Title));

            Assert.That(getMovieDto, Is.Not.Null);
        });
        Assert.That(getMovieDto.Title, Is.EqualTo(updateMovieDto.Title));
    }

    [Test]
    public void Update_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        UpdateMovieDto updateMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.UpdateAsync(movieId, updateMovieDto));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public async Task Update_WithEmptyTitle_ThrowsDataValidationException(string invalidTitle)
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);
        UpdateMovieDto updateMovieDto = new()
        {
            Title = invalidTitle
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.UpdateAsync(movieDto.Id, updateMovieDto));
    }

    [Test]
    public async Task Update_WithMaxTitleLength_ThrowsDataValidationException()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);
        UpdateMovieDto updateMovieDto = new()
        {
            Title = new string('x', 101)
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.UpdateAsync(movieDto.Id, updateMovieDto));
    }

    [Test]
    public async Task Update_WithCancelledToken_ThrowsTaskCanceledException()
{
    // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);
        UpdateMovieDto updateMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.DeleteAsync(movieDto.Id, cancellationToken));
    }

    [Test]
    public async Task Delete_WithExistingMovieId_DeletesMovie()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto movieDto = await _movieApplicationService.CreateAsync(createMovieDto);

        // Act
        await _movieApplicationService.DeleteAsync(movieDto.Id);

        // Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.GetAsync(movieDto.Id));
    }

    [Test]
    public void Delete_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.DeleteAsync(invalidId));
    }

    [Test]
    public async Task Delete_WithCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateMovieDto createMovieDto = new()
        {
            Title = $"Movie {Guid.NewGuid()}"
        };
        MovieDto newMovie = await _movieApplicationService.CreateAsync(createMovieDto);
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.DeleteAsync(newMovie.Id, cancellationToken));
    }
}