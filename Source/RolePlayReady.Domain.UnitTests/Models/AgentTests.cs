namespace RolePlayReady.Models;

public class AgentTests {
    [Fact]
    public void Constructor_WithDateTime_InitializesProperties() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new Agent(dateTime) {
            Id = Guid.NewGuid(),
            Name = "TestName",
            Description = "TestDescription",
            Inventory = new List<InventoryEntry>(),
            Journal = new List<JournalEntry>(),
        };

        agent.Inventory.Should().BeEmpty();
        agent.Journal.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new Agent {
            Id = Guid.NewGuid(),
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }
}