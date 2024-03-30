namespace RolePlayReady.Engine;

public class StepTests {
    private readonly IStepFactory _stepFactory;
    private readonly ServiceProvider _provider;

    public StepTests() {
        var services = new ServiceCollection();
        services.AddStepEngine();
        _provider = services.BuildServiceProvider();
        _stepFactory = _provider.GetRequiredService<IStepFactory>();
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
        var step = new TestStep(_stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await step.RunAsync(context);

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task RunAsync_FromInterface_Passes() {
        // Arrange
        var step = new TestStep(_stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await step.RunAsync(context);

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var step = new TestStep(_stepFactory, null);

        // Act
        await step.DisposeAsync();
        await step.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    private class TestEndStep() : EndStep<NullContext>(NullStepFactory.Instance, NullLoggerFactory.Instance) {
        public Task<NullContext> TestOnStartAsync(CancellationToken cancellation = default) => OnStartAsync(NullContext.Instance, cancellation);

        public Task<Type?> TestOnSelectNextAsync(CancellationToken cancellation = default) => OnSelectNextAsync(NullContext.Instance, cancellation);

        public Task TestOnFinishAsync(CancellationToken cancellation = default) => OnFinishAsync(NullContext.Instance, cancellation);

        public Task TestOnErrorAsync(Exception ex, CancellationToken cancellation = default) => OnErrorAsync(ex, NullContext.Instance, cancellation);
    }

    [Fact]
    public async Task OnStartAsync_IsCalled() {
        // Arrange
        var step = new TestEndStep();

        // Act
        await step.TestOnStartAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnSelectStepAsync_IsCalled() {
        // Arrange
        var step = new TestEndStep();

        // Act
        await step.TestOnSelectNextAsync();

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
        await step.TestOnErrorAsync(new("Some message."));

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}