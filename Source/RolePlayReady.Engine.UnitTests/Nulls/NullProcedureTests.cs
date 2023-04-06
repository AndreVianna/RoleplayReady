namespace RolePlayReady.Engine.Nulls;

public class NullStepTests {
    [Fact]
    public async Task RunAsync_ShouldNotThrowException() {
        await using var nullStep = NullStep.Instance;

        await nullStep.RunAsync(NullContext.Instance);
    }
}

public class NullProcedureTests {
    [Fact]
    public async Task RunAsync_ShouldNotThrowException() {
        await using var nullProcedure = NullProcedure.Instance;

        await nullProcedure.RunAsync();

        nullProcedure.Name.Should().Be(nameof(NullProcedure));
    }
}