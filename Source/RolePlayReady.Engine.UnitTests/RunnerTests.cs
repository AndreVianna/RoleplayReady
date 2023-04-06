namespace RolePlayReady.Engine;

public class RunnerTests {
    private readonly ServiceProvider _provider;
    private readonly IStepFactory _stepFactory;
    private readonly IConfigurationRoot _configuration;

    public RunnerTests() {
        var builder = new ConfigurationBuilder();
        _configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>{ {"Abc", "123" } }).Build();
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddStepEngine();
        services.AddRunner<TestRunner>();
        services.AddStep<TestStep>();
        _provider = services.BuildServiceProvider();
        _stepFactory = _provider.GetRequiredService<IStepFactory>();
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_ExecutesSteps() {
        // Arrange
        var runner = new TestRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await runner.RunAsync(context);

        // Assert
        runner.Options.Name.Should().Be(nameof(TestRunner));
        context.CurrentStepNumber.Should().Be(2);
        context.IsBlocked.Should().BeFalse();
    }


    [Fact]
    public async Task RunAsync_FromInterface_ExecutesSteps() {
        // Arrange
        var runner = new TestRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await ((IIsRunnable)runner).RunAsync(context);

        // Assert
        runner.Options.Name.Should().Be(nameof(TestRunner));
        context.CurrentStepNumber.Should().Be(2);
        context.IsBlocked.Should().BeFalse();
    }

    [Fact]
    public async Task RunAsync_WithContextInProgress_ThrowsInvalidOperationException() {
        // Arrange
        var runner = new TestRunner(_configuration, _stepFactory, null);
        var context = new Context(_provider);
        context.Block();

        // Act
        var action = () => runner.RunAsync(context);

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
        context.IsBlocked.Should().BeFalse();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_ThrowsProcedureException() {
        // Arrange
        var runner = new FaultyRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);

        // Act
        var action = () => runner.RunAsync(NullContext.Instance);

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var runner = new LongRunningRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => runner.RunAsync(NullContext.Instance, cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var runner = new TestRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);

        // Act
        await runner.DisposeAsync();
        await runner.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    private class TestEndRunner : Runner<NullContext, RunnerOptions> {
        [SetsRequiredMembers]
        public TestEndRunner(IConfiguration configuration)
            : base(configuration, NullStepFactory.Instance, NullLoggerFactory.Instance) { }

        public Task<Type?> TestOnStartAsync(CancellationToken cancellation = default)
            => OnStartAsync(NullContext.Instance, cancellation);

        public Task TestOnFinishAsync(CancellationToken cancellation = default)
            => OnFinishAsync(NullContext.Instance, cancellation);

        public Task TestOnErrorAsync(Exception ex, CancellationToken cancellation = default)
            => OnErrorAsync(ex, NullContext.Instance, cancellation);

    }

    [Fact]
    public async Task OnRunAsync_IsCalled() {
        // Arrange
        var step = new TestEndRunner(_configuration);

        // Act
        await step.TestOnStartAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnFinishAsync_IsCalled() {
        // Arrange
        var step = new TestEndRunner(_configuration);

        // Act
        await step.TestOnFinishAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnErrorAsync_IsCalled() {
        // Arrange
        var step = new TestEndRunner(_configuration);

        // Act
        await step.TestOnErrorAsync(new Exception("Some message."));

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}