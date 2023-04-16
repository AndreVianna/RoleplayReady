namespace System.Results;

public class ValidationTests {
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

        result += Success.Instance;

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
}