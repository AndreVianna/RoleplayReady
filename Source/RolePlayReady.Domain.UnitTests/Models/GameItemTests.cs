namespace RolePlayReady.Models;

public class GameItemTests {
    [Fact]
    public void Constructor_WithDateTime_InitializesProperties() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new GameItem(dateTime) {
            Id = Guid.NewGuid(),
            Name = "TestName",
            Description = "TestDescription",
            Unit = "m",
        };

        agent.Unit.Should().Be("m");
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new GameItem {
            Id = Guid.NewGuid(),
            Name = "TestName",
            Description = "TestDescription",
            Unit = "m",
        };

        agent.Should().NotBeNull();
    }
}