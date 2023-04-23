namespace RolePlayReady.Models;

public class GameSystemTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var system = new GameSystem {
            Name = "TestName",
            Description = "TestDescription",
            Domains = new List<Domain> {
                new Domain {
                    Name = "TestDomainName",
                    Description = "TestDomainDescription",
                }
            },
            ComponentDefinitions = new List<Base> {
                new Base {
                    Name = "TestDomainName",
                    Description = "TestDomainDescription",
                }
            },
        };

        system.Should().NotBeNull();
        system.Domains.Should().HaveCount(1);
        system.ComponentDefinitions.Should().HaveCount(1);
    }
}