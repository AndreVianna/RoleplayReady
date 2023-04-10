namespace RolePlayReady.Models;

public class GameSystemSettingTests {
    [Fact]
    public void Constructor_WithDateTime_InitializesProperties() {
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(DateTime.Parse("2001-01-01 00:00:00"));

        var agent = new GameSystemSetting(dateTime) {
            Name = "TestName",
            Description = "TestDescription",
            AttributeDefinitions = new List<IAttributeDefinition>(),
        };

        agent.AttributeDefinitions.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_CreatesInstance() {
        var agent = new GameSystemSetting {
            Name = "TestName",
            Description = "TestDescription"
        };

        agent.Should().NotBeNull();
    }
}