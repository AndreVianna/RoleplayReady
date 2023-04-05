using FluentAssertions.Common;

using RolePlayReady.Engine.Steps;

namespace RolePlayReady.Engine;

public class StepTests {
    private readonly ServiceCollection _services;

    public StepTests() {
        _services = new();
        _services.AddEngine();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_Throws() {
        // Arrange
        var step = new FaultyStep(_services);

        // Act
        var action = () => step.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_CancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var step = new LongRunningStep(_services);
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => step.RunAsync(cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RunAsync_WithNoErrors_Passes() {
        // Arrange
        var step = new FirstStep(_services);

        // Act
        await step.RunAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var step = new FirstStep(_services);

        // Act
        await step.DisposeAsync();
        await step.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}