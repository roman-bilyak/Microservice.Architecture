using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IUserApplicationService : IApplicationService
{
    public Task<UserListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<UserDto> GetAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<UserDto> CreateAsync([Required] CreateUserDto user, CancellationToken cancellationToken);

    public Task<UserDto> UpdateAsync([Required] Guid id, [Required] UpdateUserDto user, CancellationToken cancellationToken);

    public Task UpdatePasswordAsync([Required] Guid id, [Required] UpdateUserPasswordDto user, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<UserRoleListDto> GetRolesAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task AssignRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken);

    public Task UnassignRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken);
}