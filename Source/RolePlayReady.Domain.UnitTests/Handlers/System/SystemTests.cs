namespace RolePlayReady.Handlers.System;

public class SystemTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var system = new Handlers.System.System {
            Name = "TestName",
            Description = "TestDescription",
            Domains = [
                new() {
                    Name = "TestDomainName",
                    Description = "TestDomainDescription",
                }
            ],
        };

        system.Should().NotBeNull();
        system.Domains.Should().HaveCount(1);
    }
}