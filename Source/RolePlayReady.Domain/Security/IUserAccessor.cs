namespace RolePlayReady.Security;

public interface IUserAccessor {
    string Id { get; }
    string Email { get; }
}