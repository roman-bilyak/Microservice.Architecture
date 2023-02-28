using Microservice.Core;
using NUnit.Framework;

namespace Microservice.ReviewService.Reviews;

[TestFixture]
internal class MovieApplicationServiceTests : ReviewServiceTests
{
    private IMovieApplicationService _movieApplicationService;

    [SetUp]
    public void Setup()
    {
        _movieApplicationService = GetRequiredService<IMovieApplicationService>();
    }

    [Test]
    public async Task GetReviewList_WithValidParameters_ReturnsPage()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        int pageIndex = 0;
        int pageSize = 5;

        for (int i = 0; i < 4; i++)
        {
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Rating {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        MovieReviewListDto result = await _movieApplicationService.GetReviewListAsync(movieId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(4));
            Assert.That(result.TotalCount, Is.EqualTo(4));
        });
    }

    [Test]
    public async Task GetReviewList_WithNonExistingMovieId_ReturnsEmptyPage()
    {
        // Arrange
        Guid invalidMovieId = Guid.NewGuid();
        int pageIndex = 0;
        int pageSize = 5;

        for (int i = 0; i < 5; i++)
        {
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Rating {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(Guid.NewGuid(), createReviewDto);
        }

        // Act
        MovieReviewListDto result = await _movieApplicationService.GetReviewListAsync(invalidMovieId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task GetReviewList_WithNegativePageIndex_ReturnsFirstPage()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        int pageIndex = -5;
        int pageSize = 5;

        for (int i = 0; i < 7; i++)
        {
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Rating {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        MovieReviewListDto result = await _movieApplicationService.GetReviewListAsync(movieId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(5));
            Assert.That(result.TotalCount, Is.EqualTo(7));
        });
    }

    [Test]
    public async Task GetReviewList_WithNegativePageSize_ReturnsEmptyPage()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        int pageIndex = 0;
        int pageSize = -10;

        for (int i = 0; i < 4; i++)
        {
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Rating {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        MovieReviewListDto result = await _movieApplicationService.GetReviewListAsync(movieId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(4));
        });
    }

    [Test]
    public async Task GetReviewList_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        int pageIndex = 0;
        int pageSize = 10;
        CancellationToken canceledToken = new(true);

        for (int i = 0; i < 6; i++)
        {
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Rating {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.GetReviewListAsync(movieId, pageIndex, pageSize, canceledToken));
    }

    [Test]
    public async Task GetReview_WithExistingMovieIdAndReviewId_ReturnsReview()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act
        ReviewDto result = await _movieApplicationService.GetReviewAsync(movieId, reviewDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(reviewDto.Id));
            Assert.That(result.Text, Is.EqualTo(reviewDto.Text));
            Assert.That(result.Rating, Is.EqualTo(reviewDto.Rating));
        });
    }

    [Test]
    public async Task GetReview_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidMovieId = Guid.NewGuid();

        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.GetReviewAsync(invalidMovieId, reviewDto.Id));
    }

    [Test]
    public async Task GetReview_WithNonExistingReviewId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        Guid invalidReviewId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.GetReviewAsync(movieId, invalidReviewId));
    }

    [Test]
    public async Task GetReview_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CancellationToken canceledToken = new(true);
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Bad
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.GetReviewAsync(movieId, reviewDto.Id, canceledToken));
    }

    [Test]
    public async Task CreateReview_WithValidData_ReturnsCreatedReview()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };

        // Act
        ReviewDto result = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Text, Is.EqualTo(createReviewDto.Text));
            Assert.That(result.Rating, Is.EqualTo(createReviewDto.Rating));
        });

        ReviewDto reviewDto = await _movieApplicationService.GetReviewAsync(movieId, result.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(result.Id));
            Assert.That(result.Text, Is.EqualTo(createReviewDto.Text));
            Assert.That(result.Rating, Is.EqualTo(createReviewDto.Rating));
        });
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void CreateReview_WithEmptyText_ThrowsDataValidationException(string invalidText)
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = invalidText,
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.CreateReviewAsync(movieId, createReviewDto));
    }

    [Test]
    public void CreateReview_WithMaxTextLength_ThrowsDataValidationException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = new string('x', 501),
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.CreateReviewAsync(movieId, createReviewDto));
    }

    [Test]
    public void CreateReview_WithCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.CreateReviewAsync(movieId, createReviewDto, canceledToken));
    }

    [Test]
    public async Task UpdateReview_WithValidData_ReturnsUpdatedReview()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Bad
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        UpdateReviewDto updateReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };

        // Act
        ReviewDto result = await _movieApplicationService.UpdateReviewAsync(movieId, reviewDto.Id, updateReviewDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(reviewDto.Id));
            Assert.That(result.Text, Is.EqualTo(updateReviewDto.Text));
            Assert.That(result.Rating, Is.EqualTo(updateReviewDto.Rating));
        });
    }

    [Test]
    public async Task UpdateReview_WithSameData_ReturnsUpdatedReview()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Bad
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        UpdateReviewDto updateReviewDto = new()
        {
            Text = createReviewDto.Text,
            Rating = createReviewDto.Rating
        };

        // Act
        ReviewDto result = await _movieApplicationService.UpdateReviewAsync(movieId, reviewDto.Id, updateReviewDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(reviewDto.Id));
            Assert.That(result.Text, Is.EqualTo(createReviewDto.Text));
            Assert.That(result.Rating, Is.EqualTo(createReviewDto.Rating));
        });
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public async Task UpdateReview_WithEmptyText_ThrowsDataValidationException(string invalidText)
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Bad
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        UpdateReviewDto updateReviewDto = new()
        {
            Text = invalidText,
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.UpdateReviewAsync(movieId, reviewDto.Id, updateReviewDto));
    }

    [Test]
    public async Task UpdateReview_WithMaxTextLength_ThrowsDataValidationException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Okay
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        UpdateReviewDto updateReviewDto = new()
        {
            Text = new string('x', 502),
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _movieApplicationService.UpdateReviewAsync(movieId, reviewDto.Id, updateReviewDto));
    }

    [Test]
    public async Task UpdateReview_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.VeryGood
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        Guid invalidMovieId = Guid.NewGuid();
        UpdateReviewDto updateReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.UpdateReviewAsync(invalidMovieId, reviewDto.Id, updateReviewDto));
    }

    [Test]
    public async Task UpdateReview_WithNonExistingReviewId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.VeryBad
        };
        await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        Guid invalidReviewId = Guid.NewGuid();
        UpdateReviewDto updateReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.UpdateReviewAsync(movieId, invalidReviewId, updateReviewDto));
    }

    [Test]
    public async Task UpdateReview_WithCancelledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Bad
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        UpdateReviewDto updateReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.UpdateReviewAsync(movieId, reviewDto.Id, updateReviewDto, canceledToken));
    }

    [Test]
    public async Task DeleteReview_WithExistingMovieIdAndReviewId_DeletesReview()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act
        await _movieApplicationService.DeleteReviewAsync(movieId, reviewDto.Id);

        // Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.GetReviewAsync(movieId, reviewDto.Id));
    }

    [Test]
    public async Task DeleteReview_WithNonExistingMovieId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidMovieId = Guid.NewGuid();

        Guid movieId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.DeleteReviewAsync(invalidMovieId, reviewDto.Id));
    }

    [Test]
    public async Task DeleteReview_WithNonExistingReviewId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        Guid invalidReviewId = Guid.NewGuid();
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _movieApplicationService.DeleteReviewAsync(movieId, invalidReviewId));
    }

    [Test]
    public async Task DeleteReview_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid movieId = Guid.NewGuid();
        CancellationToken canceledToken = new(true);
        CreateReviewDto createReviewDto = new()
        {
            Text = $"Rating {Guid.NewGuid()}",
            Rating = RatingEnum.Good
        };
        ReviewDto reviewDto = await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _movieApplicationService.DeleteReviewAsync(movieId, reviewDto.Id, canceledToken));
    }
}