namespace RolePlayReady.Engine.Nulls;

public class NullRunnerTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        var nullRunner = NullRunner.Instance;

        var result = await nullRunner.RunAsync(NullContext.Instance);

        nullRunner.Name.Should().Be(nameof(NullRunner));
        nullRunner.Options.Should().BeOfType<RunnerOptions>();
        result.Should().Be(NullContext.Instance);

        await nullRunner.DisposeAsync();
    }
}