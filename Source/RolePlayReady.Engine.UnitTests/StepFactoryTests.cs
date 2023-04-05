namespace RolePlayReady.Engine;

public class StepFactoryTests {
    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var step = stepFactory.Create<EmptyContext>(typeof(FirstStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstStep>();
    }

    [Fact]
    public void Create_WithInvalidType_ThrowsInvalidCastException() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var action = () => stepFactory.Create<EmptyContext>(typeof(EmptyContext));

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_WithNullType_ReturnsNull() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var action = () => stepFactory.Create<EmptyContext>(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}