namespace System.Results;

public class SuccessTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        var success = Success.Instance;

        // Assert
        success.Should().NotBeNull();
    }
}