namespace RolePlayReady.Engine;

public class ProcedureTests {
    [Fact]
    public async Task RunAsync_WithValidSteps_Passes() {
        // Arrange
        var context = new TestContext();
        var procedure = new TestProcedure(context, typeof(FirstProcedureStep));

        // Act
        await procedure.RunAsync();

        // Assert
        procedure.Name.Should().Be("TestProcedure");
        context.StepNumber.Should().Be(2);
    }

    [Fact]
    public async Task RunAsync_WithValidSteps_AndName_Passes() {
        // Arrange
        var context = new TestContext();
        var procedure = new TestProcedure(context, "SomeName", typeof(FirstProcedureStep), new StepFactory(), NullLoggerFactory.Instance);

        // Act
        await procedure.RunAsync();

        // Assert
        procedure.Name.Should().Be("SomeName");
        context.StepNumber.Should().Be(2);
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetToThrow_Throws() {
        // Arrange
        var procedure = new FaultyProcedure(new TestContext(), typeof(FaultyProcedureStep));

        // Act
        var action = () => procedure.RunAsync();

        // Assert
        await action.Should().ThrowAsync<ProcedureException>();
    }

    [Fact]
    public async Task RunAsync_OnError_AndSetNotToThrow_Passes() {
        // Arrange
        var procedure = new FaultyProcedure(new TestContext(false), typeof(FaultyProcedureStep));

        // Act
        await procedure.RunAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetToThrow_Throws() {
        // Arrange
        var procedure = new TestProcedure(new TestContext(), typeof(LongRunningProcedureStep));
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => procedure.RunAsync(cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task RunAsync_WithCancellationRequested_AndSetNotToThrow_Passes() {
        // Arrange
        var procedure = new LongRunningProcedure(new TestContext(false), typeof(LastProcedureStep));
        var cancellationTokenSource = new CancellationTokenSource();

        // Act
        var action = () => procedure.RunAsync(cancellationTokenSource.Token);
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));

        // Assert
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        // Arrange
        var procedure = new LongRunningProcedure(new TestContext(), typeof(LastProcedureStep));

        // Act
        await procedure.DisposeAsync();
        await procedure.DisposeAsync();

        // Assert
        // No exception should be thrown, and the test should pass.
    }
}