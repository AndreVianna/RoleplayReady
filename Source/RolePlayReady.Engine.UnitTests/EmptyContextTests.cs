namespace RolePlayReady.Engine;

public class EmptyContextTests {
    [Fact]
    public void Constructor_NoParameters_CreatesEmptyContext() {
        var context = new EmptyContext();
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithServiceCollection_CreatesEmptyContext() {
        var services = new ServiceCollection();
        var context = new EmptyContext(services);
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public void EmptyContext_Services_IsNotNull() {
        var context = new EmptyContext();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        var context = new EmptyContext();

        await context.DisposeAsync();
        await context.DisposeAsync();
    }
}