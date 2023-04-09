namespace RolePlayReady.Results;

public class ValidationResultTests {
    [Fact]
    public void DefaultConstructor_IsValid() {
        // Act
        var validationResult = new ValidationResult();

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Valid_IsValid() {
        // Act
        var validationResult = ValidationResult.Valid;

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ImplicitOperator_FromValid_IsValid() {
        // Act
        ValidationResult validationResult = ResultFactory.Valid;

        // Assert
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ImplicitOperator_FromInvalid_HasErrors() {
        // Arrange
        var errors = new[] { ResultFactory.Error("Error message") };

        // Act
        ValidationResult validationResult = new Invalid(errors);

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void ImplicitOperator_FromList_HasErrors() {
        // Arrange
        var errors = new List<ValidationError?> { ResultFactory.Error("Error message") };

        // Act
        ValidationResult validationResult = errors;

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void ImplicitOperator_FromArray_HasErrors() {
        // Arrange
        var errors = new[] { ResultFactory.Error("Error message") };

        // Act
        ValidationResult validationResult = errors;

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void ImplicitOperator_FromValidationError_HasErrors() {
        // Arrange
        var error = ResultFactory.Error("Error message");

        // Act
        ValidationResult validationResult = error;

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void PlusOperator_AddValidationError_HasErrors() {
        // Arrange
        var validationResult = ValidationResult.Valid;
        var error = ResultFactory.Error("Error message");

        // Act
        validationResult += error;

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void PlusOperator_AddNullError_ThrowsArgumentException() {
        // Arrange
        var validationResult = ValidationResult.Valid;
        var error = default(ValidationError);

        // Act
        var action = () => validationResult += error;

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void PlusOperator_AddValidationErrors_HasErrors() {
        // Arrange
        var validationResult = ValidationResult.Valid;
        var errors = new[] { ResultFactory.Error("Error message") };

        // Act
        validationResult += errors;

        // Assert
        validationResult.HasErrors.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void PlusOperator_AddValidationResultToValid_HasErrors() {
        // Arrange
        var validationResult1 = ValidationResult.Valid;
        ValidationResult validationResult2 = ResultFactory.Error("Error message");

        // Act
        validationResult1 += validationResult2;

        // Assert
        validationResult1.HasErrors.Should().BeTrue();
        validationResult1.Errors.Should().HaveCount(1);
    }


    [Fact]
    public void PlusOperator_AddNullValidationResult_ThrowsArgumentException() {
        // Arrange
        var validationResult1 = ValidationResult.Valid;
        var validationResult2 = default(ValidationResult);

        // Act
        var action = () => validationResult1 += validationResult2;

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void PlusOperator_AddValidationResultToInvalid_HasErrors() {
        // Arrange
        ValidationResult validationResult1 = ResultFactory.Error("Error message 1");
        ValidationResult validationResult2 = ResultFactory.Error("Error message 2");

        // Act
        validationResult1 += validationResult2;

        // Assert
        validationResult1.HasErrors.Should().BeTrue();
        validationResult1.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void TryGetErrors_WhenHasErrors_ReturnsTrue() {
        // Arrange
        ValidationResult validationResult = ResultFactory.Error("Error message");

        // Act
        var result = validationResult.TryGetErrors(out var errors);

        // Assert
        result.Should().BeTrue();
        errors.Should().NotBeNull();
    }

    [Fact]
    public void TryGetErrors_WhenIsValid_ReturnsFalse() {
        // Arrange
        var validationResult = ValidationResult.Valid;

        // Act
        var result = validationResult.TryGetErrors(out var errors);

        // Assert
        result.Should().BeFalse();
        errors.Should().BeNull();
    }
}