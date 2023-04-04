namespace RolePlayReady.Validations;

public class InvalidTests {
    [Fact]
    public void Constructor_OneValidationError_SetsErrors() {
        // Arrange
        var error = new ValidationError("Error message");

        // Act
        var invalid = new Invalid(error);

        // Assert
        invalid.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Constructor_NullValidationError_ThrowsArgumentNullException() {
        // Arrange
        ValidationError? error = null;

        // Act
        Action act = () => _ = new Invalid(error);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("The value cannot be null. (Parameter 'error')");
    }

    [Fact]
    public void Constructor_MultipleValidationErrors_SetsErrors() {
        // Arrange
        var errors = new[] {
            new ValidationError("Error message 1"),
            new ValidationError("Error message 2")
        };

        // Act
        var invalid = new Invalid(errors);

        // Assert
        invalid.Errors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public void Constructor_NullValidationErrorCollection_ThrowsArgumentException() {
        // Arrange
        IEnumerable<ValidationError?>? errors = null;

        // Act
        Action act = () => _ = new Invalid(errors);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("The collection cannot be null or empty. (Parameter 'errors')");
    }

    [Fact]
    public void Constructor_EmptyValidationErrorCollection_ThrowsArgumentException() {
        // Arrange
        var errors = Array.Empty<ValidationError?>();

        // Act
        Action act = () => _ = new Invalid(errors);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("The collection cannot be null or empty. (Parameter 'errors')");
    }

    [Fact]
    public void Constructor_ValidationErrorCollectionWithNullElement_ThrowsArgumentException() {
        // Arrange
        var errors = new ValidationError?[] {
            new ValidationError("Error message 1"),
            null,
            new ValidationError("Error message 2")
        };

        // Act
        Action act = () => _ = new Invalid(errors);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("The collection cannot contain null items. (Parameter 'errors')");
    }
}