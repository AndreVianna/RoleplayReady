namespace RolePlayReady.Security.Abstractions;

public interface IUserAccessor {
    string Id { get; }
    string Username { get; }
    string Name { get; }
    string Email { get; }
}