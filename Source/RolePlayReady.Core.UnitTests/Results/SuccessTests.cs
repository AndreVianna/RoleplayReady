namespace System.Results;

public class SuccessTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        var success = SuccessfulResult.Success;

        // Assert
        success.Should().NotBeNull();
    }
}