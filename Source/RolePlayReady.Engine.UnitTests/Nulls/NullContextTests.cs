namespace RolePlayReady.Engine.Nulls;

public class NullContextTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        await using var nullContext = NullContext.Instance;
        nullContext.CurrentStepNumber = 2;
        nullContext.CurrentStepType = typeof(EndStep<NullContext>);
        nullContext.IsInProgress = true;

        await nullContext.ResetAsync();

        nullContext.Services.Should().NotBeNull();
        nullContext.CurrentStepNumber.Should().Be(0);
        nullContext.CurrentStepType.Should().Be(typeof(NullStep));
        nullContext.IsInProgress.Should().BeFalse();
    }
}