namespace RolePlayReady.Engine;

public class StepFactoryTests {
    private readonly ServiceCollection _serviceCollection;
    private readonly StepFactory _stepFactory;

    public StepFactoryTests() {
        _serviceCollection = new ServiceCollection();
        _stepFactory = new StepFactory(_serviceCollection);
    }

    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Act
        var step = _stepFactory.Create(typeof(FirstStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstStep>();
    }

    [Fact]
    public void Create_WithServiceProvider_ReturnsInstance() {
        // Arrange
        _serviceCollection.AddScoped<FirstStep>();

        // Act
        var step = _stepFactory.Create(typeof(FirstStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<FirstStep>();
    }

    [Fact]
    public void Create_WithInvalidType_ThrowsInvalidCastException() {
        // Act
        var action = () => _stepFactory.Create(typeof(DefaultContext));

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_WithNullType_ThrowsArgumentNullException() {
        // Act
        var action = () => _stepFactory.Create(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}