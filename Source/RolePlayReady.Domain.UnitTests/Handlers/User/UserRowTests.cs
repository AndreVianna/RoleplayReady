namespace RolePlayReady.Handlers.User;

public class UserRowRowTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new UserRow {
            Id = Guid.NewGuid(),
            Email = "some.user@email.com",
            Name = "Some UserRow",
        };

        agent.Should().NotBeNull();
    }
}