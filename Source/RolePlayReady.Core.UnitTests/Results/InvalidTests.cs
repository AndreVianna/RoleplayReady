namespace System.Results;

public class InvalidTests {
    [Fact]
    public void Constructor_OneValidationError_SetsErrors() {
        // Arrange
        var error = ResultFactory.Error("Error message", "Source");

        // Act
        var invalid = new Failure(error);

        // Assert
        invalid.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Constructor_NullValidationError_ThrowsArgumentNullException() {
        // Act
        Action act = () => _ = new Failure(default(ValidationError));

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("'error' is required. (Parameter 'error')");
    }

    [Fact]
    public void Constructor_MultipleValidationErrors_SetsErrors() {
        // Arrange
        var errors = new[] {
            ResultFactory.Error("Error message 1", "Source"),
            ResultFactory.Error("Error message 2", "Source"),
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
        act.Should().Throw<ArgumentException>().WithMessage("'errors' is required. (Parameter 'errors')");
    }

    [Fact]
    public void Constructor_EmptyValidationErrorCollection_ThrowsArgumentException() {
        // Arrange
        var errors = Array.Empty<ValidationError?>();

        // Act
        Action act = () => _ = new Failure(errors!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("'errors' cannot be empty. (Parameter 'errors')");
    }

    [Fact]
    public void Constructor_ValidationErrorCollectionWithNullElement_ThrowsArgumentException() {
        // Arrange
        var errors = new[] {
            ResultFactory.Error("Error message 1", "Source"),
            null,
            ResultFactory.Error("Error message 2", "Source"),
        };

        // Act
        Action act = () => _ = new Failure(errors!);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("'errors' cannot contain null items. (Parameter 'errors')");
    }
}