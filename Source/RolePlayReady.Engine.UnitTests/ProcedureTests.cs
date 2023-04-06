namespace RolePlayReady.Engine;

public class ProcedureTests {
    private readonly ServiceProvider _provider;
    private readonly IStepFactory _stepFactory;

    public ProcedureTests() {
        var services = new ServiceCollection();
        services.AddStepEngine();
        services.AddStep<TestStep<DefaultContext>>();
        _provider = services.BuildServiceProvider();
        _stepFactory = _provider.GetRequiredService<IStepFactory>();
    }


    [Fact]
    public void Constructor_WithContextInProgress_ThrowsArgumentException() {
        // Arrange
        var context = new DefaultContext(_provider);
        context.IsInProgress = true;

        // Act
        var action = () => _ = new TestProcedure("SomeName", context, _stepFactory);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_AndName_ExecutesSteps() {
        // Arrange
        var context = new DefaultContext(_provider);
        var procedure = new TestProcedure("SomeName", context, _stepFactory);

        // Act
        await procedure.RunAsync();

        // Assert
        procedure.Name.Should().Be("SomeName");
        context.CurrentStepNumber.Should().Be(2);
        context.IsInProgress.Should().BeFalse();
    }

    [Fact]
    public async Task RunAsync_WithContextInProgress_ThrowsInvalidOperationException() {
        // Arrange
        var context = new DefaultContext(_provider);
        var procedure = new TestProcedure("SomeName", context, _stepFactory);
        context.IsInProgress = true;

        // Act
        var action = () => procedure.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
        context.IsInProgress.Should().BeFalse();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_ThrowsProcedureException() {
        // Arrange
        var context = new DefaultContext(_provider);
        var procedure = new FaultyProcedure(context, _stepFactory);

        // Act
        var action = () => procedure.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var context = new DefaultContext(_provider);
        var procedure = new LongRunningProcedure(context, _stepFactory);
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => procedure.RunAsync(cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var context = new DefaultContext(_provider);
        var procedure = new TestProcedure("SomeName", context, _stepFactory);

        // Act
        await procedure.DisposeAsync();
        await procedure.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}