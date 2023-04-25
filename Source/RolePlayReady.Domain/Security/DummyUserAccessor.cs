namespace RolePlayReady.Security;

public class DummyUserAccessor : IUserAccessor {
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Email { get; } = "some.user@email.com";
}