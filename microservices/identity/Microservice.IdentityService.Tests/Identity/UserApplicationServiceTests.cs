using Microservice.Core;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

[TestFixture]
internal class UserApplicationServiceTests : IdentityServiceTests
{
    private IUserApplicationService _userApplicationService;
    private IRoleApplicationService _roleApplicationService;

    [SetUp]
    public void Setup()
    {
        _userApplicationService = GetRequiredService<IUserApplicationService>();
        _roleApplicationService = GetRequiredService<IRoleApplicationService>();
    }

    [Test]
    public async Task GetList_WithValidData_ReturnsUsers()
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
        Assert.ThrowsAsync<TaskCanceledException>(async () => await _userApplicationService.GetListAsync(pageIndex, pageSize, canceledToken));
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
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.GetAsync(userId));
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
        Assert.ThrowsAsync<TaskCanceledException>(async () => await _userApplicationService.GetAsync(userDto.Id, canceledToken));
    }

    [Test]
    public async Task Create_WithValidData_CreatesAndReturnsCreatedUser()
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
        Assert.ThrowsAsync<TaskCanceledException>(async () => await _userApplicationService.CreateAsync(createUserDto, canceledToken));
    }

    [Test]
    public async Task Update_WithValidData_UpdatesAndReturnsUpdatedUser()
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
        Assert.ThrowsAsync<TaskCanceledException>(async () => await _userApplicationService.UpdateAsync(userDto.Id, updateUserDto, canceledToken));
    }

    [Test]
    public async Task UpdatePassword_WithValidData_UpdatesPassword()
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
        UpdateUserPasswordDto updateUserPasswordDto = new()
        {
            OldPassword = createUserDto.Password,
            Password = $"{Guid.NewGuid()}"
        };

        // Act
        await _userApplicationService.UpdatePasswordAsync(userDto.Id, updateUserPasswordDto);

        //Assert
        UpdateUserPasswordDto updateUserPassword2Dto = new()
        {
            OldPassword = updateUserPasswordDto.Password,
            Password = updateUserPasswordDto.Password
        };
        await _userApplicationService.UpdatePasswordAsync(userDto.Id, updateUserPassword2Dto);
    }

    [Test]
    public async Task UpdatePassword_WithInvalidOldPassword_ThrowsDataValidationException()
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
        UpdateUserPasswordDto updateUserPasswordDto = new()
        {
            OldPassword = $"{Guid.NewGuid()}",
            Password = $"{Guid.NewGuid()}"
        };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(async () =>
        {
            await _userApplicationService.UpdatePasswordAsync(userDto.Id, updateUserPasswordDto);
        });
    }

    [Test]
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
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.GetAsync(userDto.Id));
    }

    [Test]
    public void Delete_WithNonExistingUserId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.DeleteAsync(userId));
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
        Assert.ThrowsAsync<TaskCanceledException>(async () => await _userApplicationService.DeleteAsync(userDto.Id, canceledToken));
    }

    [Test]
    public async Task GetRoleList_WhenExistingUserId_ReturnsUserRoles()
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

        for (int i = 0; i < 5; i++)
        {
            CreateRoleDto createRoleDto = new()
            {
                Name = $"Role {Guid.NewGuid()}"
            };
            RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

            await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);
        }

        // Act
        UserRoleListDto result = await _userApplicationService.GetRoleListAsync(userDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(5));
            Assert.That(result.TotalCount, Is.EqualTo(5));
        });
    }

    [Test]
    public void GetRoleList_WhenNonExistingUserId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidUserId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.GetRoleListAsync(invalidUserId));
    }

    [Test]
    public async Task AddRole_WithValidData_AddsRoleToUser()
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

        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        // Act
        await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);

        // Assert
        UserRoleListDto result = await _userApplicationService.GetRoleListAsync(userDto.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(1));
            Assert.That(result.TotalCount, Is.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Items[0].Id, Is.EqualTo(roleDto.Id));
            Assert.That(result.Items[0].Name, Is.EqualTo(roleDto.Name));
        });
    }

    [Test]
    public async Task AddRole_WhenNonExistingUserId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidUserId = Guid.NewGuid();
        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.AddRoleAsync(invalidUserId, roleDto.Id));
    }

    [Test]
    public async Task AddRole_WhenNonExistingRoleId_ThrowsEntityNotFoundException()
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
        Guid invalidRoleId = Guid.NewGuid();

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.AddRoleAsync(userDto.Id, invalidRoleId));
    }

    [Test]
    public async Task AddRole_WhenAlreadyAssignedToUser_ThrowsDataValidationException()
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

        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);
        await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(async () => await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id));
    }

    [Test]
    public async Task RemoveRole_WithValidData_RemovesRoleFromUser()
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

        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);
        await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);

        // Act
        await _userApplicationService.RemoveRoleAsync(userDto.Id, roleDto.Id);

        // Assert
        UserRoleListDto result = await _userApplicationService.GetRoleListAsync(userDto.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Has.Count.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task RemoveRole_WithNonExistingUserId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid invalidUserId = Guid.NewGuid();
        CreateUserDto createUserDto = new()
        {
            Name = $"{Guid.NewGuid()}",
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}",
            Email = "test@example.com",
            Password = $"{Guid.NewGuid()}",
        };
        UserDto userDto = await _userApplicationService.CreateAsync(createUserDto);

        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);
        await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.RemoveRoleAsync(invalidUserId, roleDto.Id));
    }

    [Test]
    public async Task RemoveRole_WithNonExistingRoleId_ThrowsEntityNotFoundException()
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

        Guid invalidRoleId = Guid.NewGuid();
        CreateRoleDto createRoleDto = new()
        {
            Name = $"Role {Guid.NewGuid()}"
        };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);
        await _userApplicationService.AddRoleAsync(userDto.Id, roleDto.Id);

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _userApplicationService.RemoveRoleAsync(userDto.Id, invalidRoleId));
    }
}