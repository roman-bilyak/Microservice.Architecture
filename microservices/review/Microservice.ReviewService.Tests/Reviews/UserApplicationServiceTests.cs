using NUnit.Framework;

namespace Microservice.ReviewService.Reviews;

[TestFixture]
internal class UserApplicationServiceTests : ReviewServiceTests
{
    private IMovieApplicationService _movieApplicationService;
    private IUserApplicationService _userApplicationService;

    [SetUp]
    public void Setup()
    {
        _userApplicationService = GetRequiredService<IUserApplicationService>();
        _movieApplicationService = GetRequiredService<IMovieApplicationService>();
    }

    [Test]
    public async Task GetReviewList_WithValidParameters_ReturnsPaginatedList()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = 0;
        int pageSize = 5;
        for (int i = 0; i < 5; i++)
        {
            Guid movieId = Guid.NewGuid();
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Review {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(5));
            Assert.That(result.TotalCount, Is.EqualTo(5));
        });
    }

    [Test]
    public void GetReviewList_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = 0;
        int pageSize = 10;
        CancellationToken cancellationToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize, cancellationToken));
    }

    [Test]
    public async Task GetReviewList_WithNegativePageIndex_ReturnsFirstPageList()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = -10;
        int pageSize = 5;
        for (int i = 0; i < 9; i++)
        {
            Guid movieId = Guid.NewGuid();
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Review {Guid.NewGuid()}",
                Rating = RatingEnum.Bad
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(5));
            Assert.That(result.TotalCount, Is.EqualTo(9));
        });
    }

    [Test]
    public async Task GetReviewList_WithNegativePageSize_ReturnsEmptyList()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = 0;
        int pageSize = -2;
        for (int i = 0; i < 4; i++)
        {
            Guid movieId = Guid.NewGuid();
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Review {Guid.NewGuid()}",
                Rating = RatingEnum.Bad
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(4));
        });
    }

    [Test]
    public async Task GetReviewList_WithNoReviews_ReturnsEmptyList()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = 0;
        int pageSize = 5;

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task GetReviewList_WithMultiplePages_ReturnsCorrectItemsAndTotalCount()
    {
        // Arrange
        Guid userId = Guid.Empty;
        int pageIndex = 0;
        int pageSize = 5;
        for (int i = 0; i < 7; i++)
        {
            Guid movieId = Guid.NewGuid();
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Review {Guid.NewGuid()}",
                Rating = RatingEnum.VeryGood
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(userId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(5));
            Assert.That(result.TotalCount, Is.EqualTo(7));
        });
    }

    [Test]
    public async Task GetReviewList_WithInvalidUserId_ReturnsEmptyList()
    {
        // Arrange
        Guid invalidUserId = Guid.NewGuid();
        int pageIndex = 0;
        int pageSize = 5;
        for (int i = 0; i < 7; i++)
        {
            Guid movieId = Guid.NewGuid();
            CreateReviewDto createReviewDto = new()
            {
                Text = $"Review {Guid.NewGuid()}",
                Rating = RatingEnum.Good
            };
            await _movieApplicationService.CreateReviewAsync(movieId, createReviewDto);
        }

        // Act
        UserReviewListDto result = await _userApplicationService.GetReviewListAsync(invalidUserId, pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }
}