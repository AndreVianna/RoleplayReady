namespace RolePlayReady.Engine;

public class StepFactoryTests {
    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var step = stepFactory.Create<TestContext>(typeof(FirstProcedureStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstProcedureStep>();
    }

    [Fact]
    public void Create_WithInvalidType_ThrowsInvalidCastException() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var action = () => stepFactory.Create<TestContext>(typeof(TestContext));

        // Assert
        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void Create_WithNullType_ReturnsNull() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var step = stepFactory.Create<TestContext>(null);

        // Assert
        step.Should().BeNull();
    }
}