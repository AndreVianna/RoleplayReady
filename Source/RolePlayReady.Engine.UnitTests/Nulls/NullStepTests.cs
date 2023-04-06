namespace RolePlayReady.Engine.Nulls;

public class NullStepTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        await using var nullStep = NullStep.Instance;

        await nullStep.RunAsync(NullContext.Instance);
    }
}