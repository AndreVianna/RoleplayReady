namespace RolePlayReady.Handlers.GameSystem;

public class GameSystemTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var system = new GameSystem {
            Name = "TestName",
            Description = "TestDescription",
            Domains = new List<Base> {
                new() {
                    Name = "TestDomainName",
                    Description = "TestDomainDescription",
                }
            },
        };

        system.Should().NotBeNull();
        system.Domains.Should().HaveCount(1);
    }
}