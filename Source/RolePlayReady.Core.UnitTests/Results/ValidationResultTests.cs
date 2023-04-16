namespace System.Results;

public class ValidationResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        ValidationResult result = new ValidationError("Some error.", nameof(result));

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        ValidationResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        ValidationResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromFailure_ReturnsFailure() {
        ValidationResult result = new Failure(new ValidationError("Some error.", "result"));

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        var result = new ValidationResult();

        result += SuccessfulResult.Success;

        result.IsSuccessful.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        var result = new ValidationResult();

        result += new ValidationError("Some error.", "result");

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        var result = new ValidationResult();

        result += new[] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") };

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenSuccess_ReturnsTrue() {
        // Act
        var subject = new ValidationResult();

        // Assert
        var result = subject == SuccessfulResult.Success;

        result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenFailure_ReturnsFalse() {
        // Act
        var subject = new ValidationResult(new ValidationError("Some error.", "field"));

        // Assert
        var result = subject == SuccessfulResult.Success;

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenSuccess_ReturnsFalse() {
        // Act
        var subject = new ValidationResult();

        // Assert
        var result = subject != SuccessfulResult.Success;

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenFailure_ReturnsTrue() {
        // Act
        var subject = new ValidationResult(new ValidationError("Some error.", "field"));

        // Assert
        var result = subject != SuccessfulResult.Success;

        result.Should().BeTrue();
    }
}