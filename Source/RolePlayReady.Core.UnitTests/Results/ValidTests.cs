namespace System.Results;

public class ValidTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        var valid = SuccessfulResult.Success;

        // Assert
        valid.Should().NotBeNull();
    }
}