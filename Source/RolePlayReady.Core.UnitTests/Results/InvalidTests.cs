namespace RolePlayReady.Results;

public class InvalidTests {
    [Fact]
    public void Constructor_OneValidationError_SetsErrors() {
        // Arrange
        var error = ResultFactory.Error("Error message");

        // Act
        var invalid = new Invalid(error);

        // Assert
        invalid.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Constructor_NullValidationError_ThrowsArgumentNullException() {
        // Act
        Action act = () => _ = new Invalid(default(ValidationError));

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("The value cannot be null. (Parameter 'error')");
    }

    [Fact]
    public void Constructor_MultipleValidationErrors_SetsErrors() {
        // Arrange
        var errors = new[] {
            ResultFactory.Error("Error message 1"),
            ResultFactory.Error("Error message 2"),
        };

        // Act
        var invalid = new Invalid(errors);

        // Assert
        invalid.Errors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public void Constructor_NullValidationErrorCollection_ThrowsArgumentException() {
        // Act
        Action act = () => _ = new Invalid(default(IEnumerable<ValidationError?>));

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
        var errors = new[] {
            ResultFactory.Error("Error message 1"),
            null,
            ResultFactory.Error("Error message 2"),
        };

        // Act
        Action act = () => _ = new Invalid(errors);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("The collection cannot contain null items. (Parameter 'errors')");
    }
}