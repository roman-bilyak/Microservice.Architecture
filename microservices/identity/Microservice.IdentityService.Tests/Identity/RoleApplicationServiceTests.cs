using Microservice.Core;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

[TestFixture]
internal class RoleApplicationServiceTests : IdentityServiceTests
{
    private IRoleApplicationService _roleApplicationService;

    [SetUp]
    public void Setup()
    {
        _roleApplicationService = GetRequiredService<IRoleApplicationService>();
    }

    [Test]
    public async Task GetList_WithValidData_ReturnsRoles()
    {
        // Arrange
        const int pageIndex = 3;
        const int pageSize = 5;

        RoleListDto systemRoles = await _roleApplicationService.GetListAsync(0, 5);

        const int numberOfNewRoles = 17;
        for (int i = 0; i < numberOfNewRoles; i++)
        {
            await _roleApplicationService.CreateAsync(new CreateRoleDto { Name = $"Role {i + 1}" });
        }

        // Act
        RoleListDto result = await _roleApplicationService.GetListAsync(pageIndex, pageSize);

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
    public async Task Get_WithExistingRoleId_ReturnsRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto newRoleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        // Act
        RoleDto result = await _roleApplicationService.GetAsync(newRoleDto.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(newRoleDto.Id));
            Assert.That(result.Name, Is.EqualTo(newRoleDto.Name));
        });
    }

    [Test]
    public void Get_WithNonExistingRoleId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.GetAsync(roleId));
    }

    [Test]
    public async Task Create_WithValidData_CreatesAndReturnsCreatedRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act
        RoleDto result = await _roleApplicationService.CreateAsync(createRoleDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(createRoleDto.Name));
        Assert.DoesNotThrowAsync(() => _roleApplicationService.GetAsync(result.Id));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Create_WithEmptyName_ThrowsDataValidationException(string invalidName)
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = invalidName };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.CreateAsync(createRoleDto));
    }

    [Test]
    public async Task Create_WithExistingName_ThrowsDataValidationException()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        await _roleApplicationService.CreateAsync(createRoleDto);

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.CreateAsync(createRoleDto));
    }

    [Test]
    public async Task Update_WithValidData_UpdatesAndReturnsUpdatedRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        UpdateRoleDto updateRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act
        RoleDto result = await _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(roleDto.Id));
            Assert.That(result.Name, Is.EqualTo(updateRoleDto.Name));
        });

        Assert.DoesNotThrowAsync(() => _roleApplicationService.GetAsync(result.Id));
    }

    [Test]
    public async Task Update_WithSameName_UpdatesAndReturnsUpdatedRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        UpdateRoleDto updateRoleDto = new() { Name = createRoleDto.Name };

        // Act
        RoleDto result = await _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(roleDto.Id));
            Assert.That(result.Name, Is.EqualTo(updateRoleDto.Name));
        });
    }

    [Test]
    public void Update_WithNonExistingRoleId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();
        UpdateRoleDto updateRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };

        // Act & Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.UpdateAsync(roleId, updateRoleDto));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public async Task Update_WithEmptyName_ThrowsDataValidationException(string invalidName)
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        UpdateRoleDto updateRoleDto = new() { Name = invalidName };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.UpdateAsync(roleDto.Id, updateRoleDto));
    }

    [Test]
    public async Task Update_WithExistingName_ThrowsDataValidationException()
    {
        // Arrange
        CreateRoleDto createRole1Dto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRole1Dto);

        CreateRoleDto createRole2Dto = new() { Name = $"Role {Guid.NewGuid()}" };
        await _roleApplicationService.CreateAsync(createRole2Dto);

        UpdateRoleDto updateRole1Dto = new() { Name = createRole2Dto.Name };

        // Act & Assert
        Assert.ThrowsAsync<DataValidationException>(() => _roleApplicationService.UpdateAsync(roleDto.Id, updateRole1Dto));
    }

    [Test]
    public async Task Delete_WithExistingRoleId_DeletesRole()
    {
        // Arrange
        CreateRoleDto createRoleDto = new() { Name = $"Role {Guid.NewGuid()}" };
        RoleDto roleDto = await _roleApplicationService.CreateAsync(createRoleDto);

        // Act
        await _roleApplicationService.DeleteAsync(roleDto.Id);

        // Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.GetAsync(roleDto.Id));
    }

    [Test]
    public void Delete_WithNonExistingRoleId_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid roleId = Guid.NewGuid();

        // Act and Assert
        Assert.ThrowsAsync<EntityNotFoundException>(() => _roleApplicationService.DeleteAsync(roleId));
    }
}