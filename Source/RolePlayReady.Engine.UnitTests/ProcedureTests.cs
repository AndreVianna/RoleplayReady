namespace RolePlayReady.Engine;

public class ProcedureTests {
    private readonly ServiceCollection _services;

    public ProcedureTests() {
        _services = new();
        _services.AddEngine();
    }


    [Fact]
    public void Constructor_WithContextInProgress_ThrowsArgumentException() {
        // Arrange
        var context = new DefaultContext();
        context.IsInProgress = true;

        // Act
        var action = () => _ = new TestProcedure("SomeName", context, _services);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_AndName_ExecutesSteps() {
        // Arrange
        var context = new DefaultContext();
        var procedure = new TestProcedure("SomeName", context, _services);

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
        var context = new DefaultContext();
        var procedure = new TestProcedure("SomeName", context, _services);
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
        var procedure = new FaultyProcedure(_services);

        // Act
        var action = () => procedure.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var procedure = new LongRunningProcedure(_services);
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
        var procedure = new TestProcedure("SomeName", new(), _services);

        // Act
        await procedure.DisposeAsync();
        await procedure.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}