namespace RolePlayReady.Security.Abstractions;

public interface IUserAccessor {
    string Id { get; }
    string Email { get; }
}