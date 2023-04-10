namespace RolePlayReady.Models;

public class GameSystemTests {
    [Fact]
    public void Constructor_WithDateTime_CreatesInstance() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new GameSystem(dateTime) {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new GameSystem {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }
}