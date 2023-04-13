namespace System.Results;

public class ValidationTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        Validation result = new ValidationError("Some error.", nameof(result));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        Validation result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        Validation result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromFailure_ReturnsFailure() {
        Validation result = new Failure(new ValidationError("Some error.", "result"));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        var result = new Validation();

        result += Success.Instance;

        result.IsValid.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        var result = new Validation();

        result += new ValidationError("Some error.", "result");

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        var result = new Validation();

        result += new[] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }
}