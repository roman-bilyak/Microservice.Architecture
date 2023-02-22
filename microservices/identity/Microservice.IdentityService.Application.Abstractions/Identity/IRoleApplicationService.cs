using Microservice.Application;

namespace Microservice.IdentityService.Identity;

/// <summary>
/// Provides methods for managing roles in the application.
/// </summary>
public interface IRoleApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a paginated list of roles based on the provided page index and page size.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A paginated list of roles.</returns>
    public Task<RoleListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the details of a role based on the provided role id.
    /// </summary>
    /// <param name="roleId">The id of the role to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the role.</returns>
    public Task<RoleDto> GetAsync(Guid roleId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new role based on the provided role data.
    /// </summary>
    /// <param name="role">The data for the new role.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the newly created role.</returns>
    public Task<RoleDto> CreateAsync(CreateRoleDto role, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the details of an existing role based on the provided role id and role data.
    /// </summary>
    /// <param name="roleId">The id of the role to update.</param>
    /// <param name="role">The updated data for the role.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the updated role.</returns>
    public Task<RoleDto> UpdateAsync(Guid roleId, UpdateRoleDto role, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an existing role based on the provided role id.
    /// </summary>
    /// <param name="roleId">The id of the role to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task DeleteAsync(Guid roleId, CancellationToken cancellationToken);
}