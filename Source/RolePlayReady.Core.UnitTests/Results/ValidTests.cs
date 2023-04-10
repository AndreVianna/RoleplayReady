namespace System.Results;

public class ValidTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        var valid = Valid.Instance;

        // Assert
        valid.Should().NotBeNull();
    }
}