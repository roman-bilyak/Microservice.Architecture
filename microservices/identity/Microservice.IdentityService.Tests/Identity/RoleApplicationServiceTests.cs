using Microservice.Core;
using Microservice.Tests;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

[TestFixture]
internal class RoleApplicationServiceTests : BaseIntegrationTests<IdentityServiceTestsModule>
{
    private IRoleApplicationService _roleApplicationService;

    [SetUp]
    public void Setup()
    {
        _roleApplicationService = ServiceProvider.GetRequiredService<IRoleApplicationService>();
    }

    [Test]
    public async Task GetList_ReturnsPaginatedListOfRoles()
    {
        // Arrange
        const int pageIndex = 3;
        const int pageSize = 5;

        RoleListDto systemRoles = await _roleApplicationService.GetListAsync(0, 5, CancellationToken.None);

        const int numberOfNewRoles = 17;
        for (int i = 0; i < numberOfNewRoles; i++)
        {
            await _roleApplicationService.CreateAsync(new CreateRoleDto { Name = $"Role {i + 1}" }, CancellationToken.None);
        }

        // Act
        RoleListDto result = await _roleApplicationService.GetListAsync(pageIndex, pageSize, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.TotalCount, Is.EqualTo(systemRoles.TotalCount + numberOfNewRoles));
            Assert.That(result.Items, Is.Not.Null);
        });
        long numberToReturn = systemRoles.TotalCount + numberOfNewRoles - pageIndex * pageSize;
        if (numberToReturn > pageSize)
        {
            numberToReturn = pageSize;
        }
        Assert.That(result.Items, Has.Count.EqualTo(numberToReturn));
        for (var i = 0; i < result.Items.Count; i++)
        {
            Assert.That(result.Items[i].Name, Is.EqualTo($"Role {pageIndex * pageSize - systemRoles.TotalCount + i + 1}"));
        }
    }

    [Test]
    public async Task Get_WithExistingRoleId_ReturnsRoleDto()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto newRoleDto = await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        // Act
        RoleDto result = await _roleApplicationService.GetAsync(newRoleDto.Id, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(newRoleDto.Id));
            Assert.That(result.Name, Is.EqualTo(newRoleDto.Name));
        });
    }

    [Test]
    public void Get_WithNonExistingRoleId_ThrowsException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.GetAsync(roleId, CancellationToken.None));
    }

    [Test]
    public async Task Create_WithValidData_CreatesRoleAndReturnsRoleDto()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act
        RoleDto result = await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(createRoleDto.Name));
        Assert.DoesNotThrowAsync(() => _roleApplicationService.GetAsync(result.Id, CancellationToken.None));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Create_WithEmptyName_ThrowsException(string invalidName)
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = invalidName };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None));
    }

    [Test]
    public async Task Create_WithExistingName_ThrowsException()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None));
    }

    [Test]
    public async Task Update_WithValidData_UpdatesRoleAndReturnsUpdatedRoleDto()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        UpdateRoleDto updateRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act
        RoleDto result = await _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(roleDto.Id));
            Assert.That(result.Name, Is.EqualTo(updateRoleDto.Name));
        });

        Assert.DoesNotThrowAsync(() => _roleApplicationService.GetAsync(result.Id, CancellationToken.None));
    }

    [Test]
    public async Task Update_WithSameName_UpdatesRoleAndReturnsUpdatedRoleDto()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        UpdateRoleDto updateRoleDto = new() { Name = createRoleDto.Name };

        // Act
        RoleDto result = await _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(roleDto.Id));
            Assert.That(result.Name, Is.EqualTo(updateRoleDto.Name));
        });
    }

    [Test]
    public void Update_WithNonExistingRoleId_ThrowsException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();
        UpdateRoleDto updateRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.UpdateAsync(roleId, updateRoleDto, CancellationToken.None));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public async Task Update_WithEmptyName_ThrowsException(string invalidName)
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto, CancellationToken.None);

        UpdateRoleDto updateRoleDto = new() { Name = invalidName };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto, CancellationToken.None));
    }

    [Test]
    public async Task Update_WithExistingName_ThrowsException()
    {
        // Arrange
        CreateRoleDto createRole1Dto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRole1Dto, CancellationToken.None);

        CreateRoleDto createRole2Dto = new() { Name = $"Role {Guid.NewGuid()}" };
        await _roleApplicationService.CreateAsync(createRole2Dto, CancellationToken.None);

        UpdateRoleDto updateRole1Dto = new() { Name = createRole2Dto.Name };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.UpdateAsync(roleDto.Id, updateRole1Dto, CancellationToken.None));
    }

    [Test]
    public async Task Delete_WithExistingRoleId_DeletesRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(new CreateRoleDto { Name = $"Role {Guid.NewGuid()}" }, CancellationToken.None);

        // Act
        await _roleApplicationService.DeleteAsync(roleDto.Id, CancellationToken.None);

        // Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.GetAsync(roleDto.Id, CancellationToken.None));
    }

    [Test]
    public void Delete_WithNonExistingRoleId_ThrowsException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.DeleteAsync(roleId, CancellationToken.None));
    }
}