namespace RolePlayReady.Engine;

public class StepFactoryTests {
    private readonly IStepFactory _stepFactory;

    public StepFactoryTests() {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddStepEngine();
        services.AddStep<TestStep>();
        var provider = services.BuildServiceProvider();
        _stepFactory = provider.GetRequiredService<IStepFactory>();
    }

    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Act
        var step = _stepFactory.Create(typeof(TestStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<TestStep>();
    }

    [Fact]
    public void Create_WithInvalidType_ThrowsInvalidCastException() {
        // Act
        var action = () => _stepFactory.Create(typeof(Context));

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