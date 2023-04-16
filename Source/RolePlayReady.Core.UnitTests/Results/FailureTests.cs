namespace System.Results;

public class FailureTests {
    [Fact]
    public void Constructor_OneValidationError_SetsErrors() {
        // Arrange
        var error = new ValidationError("Error message", "Source");

        // Act
        var invalid = new Failure(error);

        // Assert
        invalid.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Constructor_NullValidationError_ThrowsArgumentNullException() {
        // Act
        Action act = () => _ = new Failure(default(ValidationError)!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("'validationError' cannot be null. (Parameter 'validationError')");
    }

    [Fact]
    public void Constructor_MultipleValidationErrors_SetsErrors() {
        // Arrange
        var errors = new[] {
            new ValidationError("Error message 1", "Source"),
            new ValidationError("Error message 2", "Source"),
        };

        // Act
        var invalid = new Failure(errors);

        // Assert
        invalid.Errors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public void Constructor_NullValidationErrorCollection_ThrowsArgumentException() {
        // Act
        Action act = () => _ = new Failure(default(ICollection<ValidationError?>)!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("'validationErrors' cannot be null. (Parameter 'validationErrors')");
    }

    [Fact]
    public void Constructor_EmptyValidationErrorCollection_ThrowsArgumentException() {
        // Arrange
        var errors = Array.Empty<ValidationError?>();

        // Act
        Action act = () => _ = new Failure(errors!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("'validationErrors' cannot be empty. (Parameter 'validationErrors')");
    }

    [Fact]
    public void Constructor_ValidationErrorCollectionWithNullElement_ThrowsArgumentException() {
        // Arrange
        var errors = new[] {
            new ValidationError("Error message 1", "Source"),
            null,
            new ValidationError("Error message 2", "Source"),
        };

        // Act
        Action act = () => _ = new Failure(errors!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("'validationErrors' cannot contain null items. (Parameter 'validationErrors')");
    }
}