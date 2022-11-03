using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IUserApplicationService : IApplicationService
{
    public Task<UserDto> GetUserAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<UserListDto> GetUsersAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<UserDto> CreateUserAsync([Required] CreateUserDto user, CancellationToken cancellationToken);

    public Task<UserDto> UpdateUserAsync([Required] Guid id, [Required] UpdateUserDto user, CancellationToken cancellationToken);

    public Task ChangeUserPasswordAsync([Required] Guid id, [Required] ChangeUserPasswordDto user, CancellationToken cancellationToken);

    public Task<UserRoleListDto> GetUserRolesAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task AddUserToRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken);

    public Task RemoveUserFromRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken);

    public Task DeleteUserAsync([Required] Guid id, CancellationToken cancellationToken);
}