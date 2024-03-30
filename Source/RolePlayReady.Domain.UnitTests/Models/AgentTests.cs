namespace RolePlayReady.Models;

public class AgentTests {
    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Agent {
            Name = "TestName",
            Description = "TestDescription",
            Inventory = [],
            Journal = [],
        };

        agent.Should().NotBeNull();
        agent.Inventory.Should().BeEmpty();
        agent.Journal.Should().BeEmpty();
    }
}