namespace RolePlayReady.Engine;

public class SimpleRunnerTests {
    private readonly ServiceProvider _provider;
    private readonly IStepFactory _stepFactory;
    private readonly IConfigurationRoot _configuration;

    public SimpleRunnerTests() {
        var builder = new ConfigurationBuilder();
        _configuration = builder.AddInMemoryCollection(new Dictionary<string, string?> { { "Abc", "123" } }).Build();
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddStepEngine();
        services.AddRunner<TestSimpleRunner>();
        services.AddStep<TestStep>();
        _provider = services.BuildServiceProvider();
        _stepFactory = _provider.GetRequiredService<IStepFactory>();
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_ExecutesSteps() {
        // Arrange
        var runner = new TestSimpleRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await runner.RunAsync(context);

        // Assert
        runner.Options.Name.Should().Be(nameof(TestRunnerOptions));
        context.CurrentStepNumber.Should().Be(0);
        context.IsBlocked.Should().BeFalse();
    }


    [Fact]
    public async Task RunAsync_FromInterface_ExecutesSteps() {
        // Arrange
        var runner = new TestSimpleRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await runner.RunAsync(context);

        // Assert
        runner.Options.Name.Should().Be(nameof(TestRunnerOptions));
        context.CurrentStepNumber.Should().Be(0);
        context.IsBlocked.Should().BeFalse();
    }

    [Fact]
    public async Task RunAsync_WithContextInProgress_ThrowsInvalidOperationException() {
        // Arrange
        var runner = new TestSimpleRunner(_configuration, _stepFactory, null);
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
        var runner = new FaultyStepRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);

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
        var runner = new TestSimpleRunner(_configuration, _stepFactory, NullLoggerFactory.Instance);

        // Act
        await runner.DisposeAsync();
        await runner.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnRunAsync_IsCalled() {
        // Arrange
        var step = new TestSimpleRunner(_configuration, NullStepFactory.Instance, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await step.TestOnRunAsync(context);

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task OnErrorAsync_IsCalled() {
        // Arrange
        var step = new TestSimpleRunner(_configuration, NullStepFactory.Instance, NullLoggerFactory.Instance);
        var context = new Context(_provider);

        // Act
        await step.TestOnErrorAsync(new Exception("Some message."), context);

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}