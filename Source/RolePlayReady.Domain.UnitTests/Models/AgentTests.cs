namespace RolePlayReady.Models;

public class AgentTests {
    [Fact]
    public void Constructor_WithDateTime_InitializesProperties() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new Agent(dateTime) {
            Name = "TestName",
            Description = "TestDescription",
            Inventory = new List<IInventoryEntry>(),
            Journal = new List<IJournalEntry>(),
        };

        agent.Inventory.Should().BeEmpty();
        agent.Journal.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Agent {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }
}