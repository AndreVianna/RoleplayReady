using Microsoft.Extensions.DependencyInjection;

namespace RolePlayReady.Engine;

public class EmptyContextTests {
    [Fact]
    public void Constructor_NoParameters_CreatesEmptyContext() {
        var context = new EmptyContext();
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
        context.ThrowsOnError.Should().BeTrue();
    }

    [Fact]
    public void Constructor_ThrowsOnError_CreatesEmptyContext() {
        var context = new EmptyContext(false);
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
        context.ThrowsOnError.Should().BeFalse();
    }

    [Fact]
    public void Constructor_ServiceProvider_CreatesEmptyContext() {
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        var context = new EmptyContext(serviceProvider);
        context.Should().NotBeNull();
        context.Services.Should().NotBeNull();
        context.ThrowsOnError.Should().BeTrue();
    }

    [Fact]
    public void EmptyContext_Silent_IsNotNull() {
        EmptyContext.Silent.Should().NotBeNull();
        EmptyContext.Silent.ThrowsOnError.Should().BeFalse();
    }

    [Fact]
    public void EmptyContext_Default_IsNotNull() {
        EmptyContext.Default.Should().NotBeNull();
        EmptyContext.Default.ThrowsOnError.Should().BeTrue();
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