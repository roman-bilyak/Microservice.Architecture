using Microservice.Application;

namespace Microservice.IdentityService.Identity;

/// <summary>
/// Provides methods for managing users in the application.
/// </summary>
public interface IUserApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a paginated list of users based on the provided page index and page size.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A paginated list of users.</returns>
    public Task<UserListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the details of a user based on the provided user id.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the user.</returns>
    public Task<UserDto> GetAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new user based on the provided user data.
    /// </summary>
    /// <param name="user">The data for the new user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the newly created user.</returns>
    public Task<UserDto> CreateAsync(CreateUserDto user, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the details of an existing user based on the provided user id and user data.
    /// </summary>
    /// <param name="userId">The id of the user to update.</param>
    /// <param name="user">The updated data for the user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the updated user.</returns>
    public Task<UserDto> UpdateAsync(Guid userId, UpdateUserDto user, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the password of an existing user based on the provided user id and password data.
    /// </summary>
    /// <param name="userId">The id of the user to update.</param>
    /// <param name="user">The updated password data for the user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task UpdatePasswordAsync(Guid userId, UpdateUserPasswordDto user, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an existing user based on the provided user id.
    /// </summary>
    /// <param name="userId">The id of the user to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task DeleteAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of roles assigned to the specified user.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve roles for.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A list of roles assigned to the user.</returns>
    public Task<UserRoleListDto> GetRoleListAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a role to the user identified by the provided user id.
    /// </summary>
    /// <param name="userId">The id of the user to add the role to.</param>
    /// <param name="roleName">The name of the role to add.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task AddRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a role from the user identified by the provided user id.
    /// </summary>
    /// <param name="userId">The id of the user to remove the role from.</param>
    /// <param name="roleName">The name of the role to remove.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task RemoveRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken);
}