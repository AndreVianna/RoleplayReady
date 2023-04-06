namespace RolePlayReady.Engine.Nulls;

public class NullContextTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        await using var nullContext = NullContext.Instance;

        nullContext.Block();
        await nullContext.InitializeAsync();
        await nullContext.UpdateAsync(null!);
        nullContext.Release();

        nullContext.Services.Should().NotBeNull();
        nullContext.CurrentStepNumber.Should().Be(0);
        nullContext.CurrentStep.Should().BeNull();
        nullContext.IsBlocked.Should().BeFalse();
    }
}