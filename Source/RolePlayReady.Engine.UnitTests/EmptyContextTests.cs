namespace RolePlayReady.Engine;

public class EmptyContextTests {
    [Fact]
    public void Constructor_NoParameters_CreatesEmptyContext() {
        var context = new DefaultContext();
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithServiceCollection_CreatesEmptyContext() {
        var context = new DefaultContext(NullServiceCollection.Instance);
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public void EmptyContext_Services_IsNotNull() {
        var context = new DefaultContext();
        context.Services.Should().NotBeNull();
    }

    [Fact]
    public async Task DisposeAsync_CalledMultipleTimes_Passes() {
        var context = new DefaultContext();

        await context.DisposeAsync();
        await context.DisposeAsync();
    }
}