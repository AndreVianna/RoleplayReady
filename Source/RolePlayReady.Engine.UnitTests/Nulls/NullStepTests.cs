namespace RolePlayReady.Engine.Nulls;

public class NullStepTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        var nullStep = NullStep.Instance;

        await nullStep.RunAsync(NullContext.Instance);

        await nullStep.DisposeAsync();
    }
}