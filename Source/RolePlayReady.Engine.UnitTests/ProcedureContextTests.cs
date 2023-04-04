namespace RolePlayReady.Engine;

public class ProcedureContextTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_ThrowOnErrorIsSet_ProcedureContextInitialized(bool throwOnError) {
        // Act
        var context = new TestContext(throwOnError);

        // Assert
        context.ThrowOnError.Should().Be(throwOnError);
    }

    [Fact]
    public void IncrementStepNumber_WhenCalled_IncrementsStepNumber() {
        // Arrange
        var context = new TestContext();

        // Act
        context.IncrementStepNumber();

        // Assert
        context.StepNumber.Should().Be(1);
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new TestContext();

        // Act
        await context.DisposeAsync();
        await context.DisposeAsync();

        // Assert
        // No exception should be thrown
    }
}