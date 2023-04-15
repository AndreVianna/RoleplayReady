namespace System.DependencyInjection;

public class NullServiceCollectionTests {
    [Fact]
    public void Instance_NotNull() {
        // Arrange & Act
        var instance = NullServiceCollection.Instance;

        // Assert
        instance.Should().NotBeNull();
    }
}