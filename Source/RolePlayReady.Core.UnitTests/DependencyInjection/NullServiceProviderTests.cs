namespace RolePlayReady.DependencyInjection;

public class NullServiceProviderTests {
    [Fact]
    public void Instance_NotNull() {
        // Arrange & Act
        var instance = NullServiceProvider.Instance;

        // Assert
        instance.Should().NotBeNull();
    }
}