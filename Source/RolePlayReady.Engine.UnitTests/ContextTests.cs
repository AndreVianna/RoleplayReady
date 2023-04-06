namespace RolePlayReady.Engine;

public class ContextTests {
    private readonly ServiceProvider _provider;

    public ContextTests() {
        var services = new ServiceCollection();
        services.AddStepEngine();
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void Services_ReturnsServices() {
        // Act
        var context = new Context(_provider);

        // Assert
        context.Services.Should().Be(_provider);
    }

    [Fact]
    public void Block_WhenCalled_BlocksContext() {
        // Arrange
        var context = new Context(_provider);

        // Act
        context.Block();

        // Assert
        context.IsBlocked.Should().BeTrue();
    }

    [Fact]
    public async Task IncrementStepNumber_WhenCalled_IncrementsStepNumber() {
        // Arrange
        var context = new Context(_provider);
        var step = new EndStep<Context>(NullStepFactory.Instance, NullLoggerFactory.Instance);

        // Act
        await context.UpdateAsync(step);

        // Assert
        context.CurrentStepNumber.Should().Be(1);
        context.CurrentStep.Should().Be(step);
    }

    [Fact]
    public async Task InitializeAsync_WhenCalled_ResetContextInfo() {
        // Arrange
        var context = new Context(_provider);

        // Act
        await context.InitializeAsync();

        // Assert
        context.CurrentStepNumber.Should().Be(0);
        context.CurrentStep.Should().BeNull();
    }

    [Fact]
    public void Release_WhenCalled_UnblocksContext() {
        // Arrange
        var context = new Context(_provider);

        // Act
        context.Release();

        // Assert
        context.IsBlocked.Should().BeFalse();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new Context(_provider);

        // Act
        await context.DisposeAsync();
        await context.DisposeAsync();

        // Assert
        // No exception should be thrown
    }
}