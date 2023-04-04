namespace RolePlayReady.Validations;

public class ValidTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        var valid = new Valid();

        // Assert
        valid.Should().NotBeNull();
    }
}