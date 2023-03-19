namespace Microservice.Core;

public interface ICurrentUser
{
    Guid Id { get; }

    string Name { get; }

    string FirstName { get; }

    string LastName { get; }

    string Email { get; }

    bool IsAuthenticated { get; }
}