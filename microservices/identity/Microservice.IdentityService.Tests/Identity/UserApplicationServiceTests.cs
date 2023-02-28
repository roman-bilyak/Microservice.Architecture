using Microservice.Core;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

[TestFixture]
internal class UserApplicationServiceTests : IdentityServiceTests
{
    private IUserApplicationService _userApplicationService;

    [SetUp]
    public void Setup()
    {
        _userApplicationService = GetRequiredService<IUserApplicationService>();
    }

    [Test]
    public async Task GetList_WithValidParameters_ReturnsSystemUsers()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 10;

        // Act
        UserListDto result = await _userApplicationService.GetListAsync(pageIndex, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(2));
            Assert.That(result.TotalCount, Is.EqualTo(2));
        });
    }

    [Test]
    public void GetList_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 10;
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.GetListAsync(pageIndex, pageSize, canceledToken));
    }

    [Test]
    public async Task Get_WithExistingUserId_ReturnsUser()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);

        // Act
        UserDto result = await _userApplicationService.GetAsync(userDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(userDto.Id));
            Assert.That(result.Name, Is.EqualTo(userDto.Name));
            Assert.That(result.FirstName, Is.EqualTo(userDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(userDto.LastName));
            Assert.That(result.Email, Is.EqualTo(userDto.Email));
            Assert.That(result.IsEmailConfirmed, Is.EqualTo(false));
        });
    }

    [Test]
    public void Get_WithNonExistingUserId_ThrowsEntitytNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplicationService.GetAsync(userId));
    }

    [Test]
    public async Task Get_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.GetAsync(userDto.Id, canceledToken));
    }

    [Test]
    public async Task Create_WithValidData_CreatesAndReturnsUser()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };

        // Act
        UserDto result = await _userApplicationService.CreateAsync(createUserDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(createUserDto.Name));
            Assert.That(result.FirstName, Is.EqualTo(createUserDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(createUserDto.LastName));
            Assert.That(result.Email, Is.EqualTo(createUserDto.Email));
            Assert.That(result.IsEmailConfirmed, Is.EqualTo(false));
        });
    }

    [Test]
    public void Create_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.CreateAsync(createUserDto, canceledToken));
    }

    [Test]
    public async Task Update_WithValidData_UpdatesAndReturnsUser()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);
        UpdateUserDto updateUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "update@example.com"
        };

        // Act
        UserDto result = await _userApplicationService.UpdateAsync(userDto.Id, updateUserDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(updateUserDto.Name));
            Assert.That(result.FirstName, Is.EqualTo(updateUserDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(updateUserDto.LastName));
            Assert.That(result.Email, Is.EqualTo(updateUserDto.Email));
            Assert.That(result.IsEmailConfirmed, Is.EqualTo(false));
        });
    }

    [Test]
    public async Task Update_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);
        UpdateUserDto updateUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "update@example.com"
        };
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.UpdateAsync(userDto.Id, updateUserDto, canceledToken));
    }

    public async Task Delete_WithExistingUserId_DeletesRole()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);

        // Act
        await _userApplicationService.DeleteAsync(userDto.Id);

        // Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplicationService.GetAsync(userDto.Id));
    }

    [Test]
    public void Delete_WithNonExistingUserId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplicationService.DeleteAsync(userId));
    }

    [Test]
    public async Task Delete_WithCanceledToken_ThrowsTaskCanceledException()
    {
        // Arrange
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);
        CancellationToken canceledToken = new(true);

        // Act & Assert
        Assert.ThrowsAsync<TaskCanceledException>(() => _userApplicationService.DeleteAsync(userDto.Id, canceledToken));
    }
}