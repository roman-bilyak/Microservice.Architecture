using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IUserApplicationService : IApplicationService
{
    public Task<UserListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<UserDto> GetAsync([Required] Guid userId, CancellationToken cancellationToken);

    public Task<UserDto> CreateAsync([Required] CreateUserDto user, CancellationToken cancellationToken);

    public Task<UserDto> UpdateAsync([Required] Guid userId, [Required] UpdateUserDto user, CancellationToken cancellationToken);

    public Task UpdatePasswordAsync([Required] Guid userId, [Required] UpdateUserPasswordDto user, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid userId, CancellationToken cancellationToken);

    public Task<UserRoleListDto> GetRoleListAsync([Required] Guid userId, CancellationToken cancellationToken);

    public Task AddRoleAsync([Required] Guid userId, [Required] string roleName, CancellationToken cancellationToken);

    public Task RemoveRoleAsync([Required] Guid userId, [Required] string roleName, CancellationToken cancellationToken);
}