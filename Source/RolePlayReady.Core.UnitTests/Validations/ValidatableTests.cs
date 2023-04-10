namespace System.Validations;

public class ValidatableTests {
    private class TestValidatable : Validatable { }

    [Fact]
    public void Validate_WithoutContext_ReturnsValid() {
        // Arrange
        var validatable = new TestValidatable();

        // Act
        var validationResult = validatable.Validate<object>();

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithContext_ReturnsValid() {
        // Arrange
        var validatable = new TestValidatable();
        var context = new object();

        // Act
        var validationResult = validatable.Validate(context);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }
}