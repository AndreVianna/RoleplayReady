namespace RolePlayReady.Engine.Nulls;

public class NullRunnerTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        await using var nullProcedure = NullRunner.Instance;

        var result = await nullProcedure.RunAsync(NullContext.Instance);

        nullProcedure.Name.Should().Be(nameof(NullRunner));
        nullProcedure.Options.Should().BeOfType<RunnerOptions>();
        result.Should().Be(NullContext.Instance);
    }
}