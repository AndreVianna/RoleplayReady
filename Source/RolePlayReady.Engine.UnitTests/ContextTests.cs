namespace RolePlayReady.Engine;

public class ContextTests {
    [Fact]
    public void IncrementStepNumber_WhenCalled_IncrementsStepNumber() {
        // Arrange
        var context = new EmptyContext();

        // Act
        context.CurrentStepNumber++;
        context.CurrentStepType = typeof(FirstStep);

        // Assert
        context.CurrentStepNumber.Should().Be(1);
        context.CurrentStepType.Should().Be(typeof(FirstStep));
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new EmptyContext();

        // Act
        await context.DisposeAsync();
        await context.DisposeAsync();

        // Assert
        // No exception should be thrown
    }
}