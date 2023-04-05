namespace RolePlayReady.Engine;

public class EndStepTests {
    [Fact]
    public async Task OnRunAsync_ShouldReturnNull() {
        var endStep = new TestEndStep();
        var result = await endStep.TestOnRunAsync();
        result.Should().BeNull();
    }

    [Fact]
    public async Task OnFinishAsync_ShouldNotThrowException() {
        var endStep = new TestEndStep();
        Func<Task> act = async () => await endStep.TestOnFinishAsync();
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task OnErrorAsync_ShouldNotThrowException() {
        var endStep = new TestEndStep();
        var exception = new Exception("Test exception");
        Func<Task> act = async () => await endStep.TestOnErrorAsync(exception);
        await act.Should().NotThrowAsync<Exception>();
    }
}