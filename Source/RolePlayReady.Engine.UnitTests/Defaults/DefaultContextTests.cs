namespace RolePlayReady.Engine.Defaults;

public class EmptyContextTests {
    [Fact]
    public void Constructor_WithServiceProvider_CreatesEmptyContext() {
        var context = new DefaultContext(NullServiceProvider.Instance);
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        var context = new DefaultContext(NullServiceProvider.Instance);

        await context.DisposeAsync();
        await context.DisposeAsync();
    }
}