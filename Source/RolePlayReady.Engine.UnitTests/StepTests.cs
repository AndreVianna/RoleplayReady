namespace RolePlayReady.Engine;

public class StepTests {
    private readonly IStepFactory _stepFactory;

    public StepTests() {
        var services = new ServiceCollection();
        services.AddEngine();
        var provider = services.BuildServiceProvider();
        _stepFactory = provider.GetRequiredService<IStepFactory>();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_Throws() {
        // Arrange
        var step = new FaultyStep(_stepFactory);

        // Act
        var action = () => step.RunAsync(NullContext.Instance);

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var step = new LongRunningStep(_stepFactory);
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(NullContext.Instance, cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RunAsync_WithNoErrors_Passes() {
        // Arrange
        var step = new EndStep(_stepFactory, NullLoggerFactory.Instance);

        // Act
        await step.RunAsync(NullContext.Instance);

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var step = new EndStep(_stepFactory);

        // Act
        await step.DisposeAsync();
        await step.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    private class TestEndStep : EndStep {
        public TestEndStep() : base(NullStepFactory.Instance, NullLoggerFactory.Instance) { }

        public Task<Type?> TestOnRunAsync(CancellationToken cancellation = default)
            => OnRunAsync(cancellation);

        public Task TestOnErrorAsync(Exception ex, CancellationToken cancellation = default)
            => OnErrorAsync(ex, cancellation);

        public Task TestOnFinishAsync(CancellationToken cancellation = default)
            => OnFinishAsync(cancellation);

    }

    [Fact]
    public async Task OnRunAsync_IsCalled() {
        // Arrange
        var step = new TestEndStep();

        // Act
        await step.TestOnRunAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnFinishAsync_IsCalled() {
        // Arrange
        var step = new TestEndStep();

        // Act
        await step.TestOnFinishAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnErrorAsync_IsCalled() {
        // Arrange
        var step = new TestEndStep();

        // Act
        await step.TestOnErrorAsync(new Exception("Some message."));

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}