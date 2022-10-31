using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IUserApplicationService : IApplicationService
{
    public Task<UserDto> GetUserAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<UserListDto> GetUsersAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<UserDto> CreateUserAsync([Required] CreateUserDto user, CancellationToken cancellationToken);

    public Task<UserDto> UpdateUserAsync([Required] Guid id, [Required] UpdateUserDto user, CancellationToken cancellationToken);

    public Task DeleteRoleAsync([Required] Guid id, CancellationToken cancellationToken);
}