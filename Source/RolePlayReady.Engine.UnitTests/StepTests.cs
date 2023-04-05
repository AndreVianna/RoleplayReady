namespace RolePlayReady.Engine;

public class StepTests {
    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_Throws() {
        // Arrange
        var step = new FaultyStep();

        // Act
        var action = () => step.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetNotToThrow_Passes() {
        // Arrange
        var step = new FaultyStep();

        // Act
        await step.RunAsync(false);

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var step = new LongRunningStep();
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetNotToThrow_Passes() {
        // Arrange
        var step = new LongRunningStep();
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(false, cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(50));

        // Assert
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task RunAsync_WithNoErrors_Passes() {
        // Arrange
        var step = new FirstStep();

        // Act
        var action = async () => await step.RunAsync(true);

        // Assert
        await action.Should().NotThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var step = new FirstStep();

        // Act
        await step.DisposeAsync();
        await step.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}