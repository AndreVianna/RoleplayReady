namespace RolePlayReady.Engine;

public class StepFactoryTests {
    private readonly IStepFactory _stepFactory;
    private readonly ServiceCollection _services;

    public StepFactoryTests() {
        _services = new ServiceCollection();
        _services.AddEngine();
        var provider = _services.BuildServiceProvider();
        _stepFactory = provider.GetRequiredService<IStepFactory>();
    }

    [Fact]
    public void Create_WithValidType_ReturnsInstance() {
        // Act
        _services.AddStep<TestStep>();
        var provider = _services.BuildServiceProvider();
        var stepFactory = provider.GetRequiredService<IStepFactory>();

        var step = stepFactory.Create(typeof(TestStep));

        // Assert
        step.Should().NotBeNull();
        step.Should().BeOfType<TestStep>();
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