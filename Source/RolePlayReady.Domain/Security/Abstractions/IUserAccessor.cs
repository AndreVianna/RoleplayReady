namespace RolePlayReady.Security.Abstractions;

public interface IUserAccessor {
    string Id { get; }
    string BaseFolder { get; }
    string Email { get; }
}