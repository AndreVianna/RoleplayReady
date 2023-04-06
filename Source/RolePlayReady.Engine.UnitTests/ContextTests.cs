namespace RolePlayReady.Engine;

public class ContextTests {
    private readonly ServiceProvider _provider;

    public ContextTests() {
        var services = new ServiceCollection();
        services.AddEngine();
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void IncrementStepNumber_WhenCalled_IncrementsStepNumber() {
        // Arrange
        var context = new DefaultContext(_provider);

        // Act
        context.CurrentStepNumber++;
        context.CurrentStepType = typeof(EndStep);

        // Assert
        context.CurrentStepNumber.Should().Be(1);
        context.CurrentStepType.Should().Be(typeof(EndStep));
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new DefaultContext(_provider);

        // Act
        await context.DisposeAsync();
        await context.DisposeAsync();

        // Assert
        // No exception should be thrown
    }
}