namespace RolePlayReady.Engine;

public class ProcedureContextTests {
    private class TestProcedureContext : ProcedureContext<TestProcedureContext> {
        [SetsRequiredMembers]
        public TestProcedureContext(bool throwOnError = true)
            : base(throwOnError) { }
    }

    [Fact]
    public void Constructor_ThrowOnErrorIsSet_ProcedureContextInitialized() {
        // Arrange
        var throwOnError = true;

        // Act
        var context = new TestProcedureContext(throwOnError);

        // Assert
        context.ThrowOnError.Should().Be(throwOnError);
    }

    [Fact]
    public void IncrementStepNumber_StepNumberIsIncremented() {
        // Arrange
        var context = new TestProcedureContext();

        // Act
        context.IncrementStepNumber();

        // Assert
        context.StepNumber.Should().Be(1);
    }

    [Fact]
    public void Dispose_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new TestProcedureContext();

        // Act
        context.Dispose();
        context.Dispose();

        // Assert
        // No exception should be thrown
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_ContextDisposed() {
        // Arrange
        var context = new TestProcedureContext();

        // Act
        await context.DisposeAsync();
        await context.DisposeAsync();

        // Assert
        // No exception should be thrown
    }
}