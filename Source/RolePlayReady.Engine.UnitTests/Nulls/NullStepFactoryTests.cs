namespace RolePlayReady.Engine.Nulls;

public class NullStepFactoryTests {
    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Arrange
        var stepFactory = NullStepFactory.Instance;

        // Act
        var step = stepFactory.Create(typeof(EndStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<NullStep>();
    }
}