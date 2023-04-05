namespace RolePlayReady.Engine;

public class ProcedureTests {
    [Fact]
    public async Task RunAsync_WithValidSteps_ExecutesSteps() {
        // Arrange
        var context = new EmptyContext();
        var procedure = new TestProcedure(context);

        // Act
        await procedure.RunAsync();

        // Assert
        procedure.Name.Should().Be("TestProcedure");
        context.CurrentStepNumber.Should().Be(2);
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_AndName_ExecutesSteps() {
        // Arrange
        var context = new EmptyContext();
        var procedure = new TestProcedure("SomeName", context, new StepFactory(), NullLoggerFactory.Instance);

        // Act
        await procedure.RunAsync();

        // Assert
        procedure.Name.Should().Be("SomeName");
        context.CurrentStepNumber.Should().Be(2);
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_ThrowsProcedureException() {
        // Arrange
        var procedure = new FaultyProcedure();

        // Act
        var action = () => procedure.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var procedure = new LongRunningProcedure();
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
        var procedure = new TestProcedure(new());

        // Act
        await procedure.DisposeAsync();
        await procedure.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}