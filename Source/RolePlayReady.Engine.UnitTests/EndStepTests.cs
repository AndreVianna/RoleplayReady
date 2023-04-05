namespace RolePlayReady.Engine;

public class EndStepTests {
    private readonly ServiceCollection _services;

    public EndStepTests() {
        _services = new();
        _services.AddEngine();
    }

    [Fact]
    public async Task OnRunAsync_ShouldReturnNull() {
        var endStep = new TestEndStep(_services);
        var result = await endStep.TestOnRunAsync();
        result.Should().BeNull();
    }

    [Fact]
    public async Task OnFinishAsync_ShouldNotThrowException() {
        var endStep = new TestEndStep(_services);
        Func<Task> act = async () => await endStep.TestOnFinishAsync();
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task OnErrorAsync_ShouldNotThrowException() {
        var endStep = new TestEndStep(_services);
        var exception = new Exception("Test exception");
        Func<Task> act = async () => await endStep.TestOnErrorAsync(exception);
        await act.Should().NotThrowAsync<Exception>();
    }
}