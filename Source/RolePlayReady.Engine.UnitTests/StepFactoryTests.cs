namespace RolePlayReady.Engine;

public class StepFactoryTests {
    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var step = stepFactory.Create(typeof(FirstStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstStep>();
    }

    [Fact]
    public void Create_WithServiceProvider_ReturnsInstance() {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<FirstStep>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var stepFactory = new StepFactory(serviceCollection);

        // Act
        var step = stepFactory.Create(typeof(FirstStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstStep>();
    }

    [Fact]
    public void Create_WithInvalidType_ThrowsInvalidCastException() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var action = () => stepFactory.Create(typeof(EmptyContext));

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_WithNullType_ThrowsArgumentNullException() {
        // Arrange
        var stepFactory = new StepFactory();

        // Act
        var action = () => stepFactory.Create(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}