namespace RolePlayReady.Engine;

public class ProcedureStepTests {
    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_Throws() {
        // Arrange
        var step = new FaultyProcedureStep();

        // Act
        var action = () => step.RunAsync(new TestContext());

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetNotToThrow_Passes() {
        // Arrange
        var step = new FaultyProcedureStep();

        // Act
        await step.RunAsync(new TestContext(false));

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var step = new LongRunningProcedureStep();
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(new TestContext(), cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetNotToThrow_Passes() {
        // Arrange
        var step = new LongRunningProcedureStep();
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(new TestContext(false), cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(50));

        // Assert
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task RunAsync_WithNoErrors_Passes() {
        // Arrange
        var step = new FirstProcedureStep();

        // Act
        var action = async () => await step.RunAsync(new TestContext());

        // Assert
        await action.Should().NotThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var step = new FirstProcedureStep();

        // Act
        await step.DisposeAsync();
        await step.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}