using static System.Results.ValidationResult;

namespace System.Results;

public class ValidationResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        ValidationResult result = new ValidationError("Some error.", nameof(result));

        result.IsSuccess.Should().BeFalse();
        result.HasValidationErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        ValidationResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasValidationErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        ValidationResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasValidationErrors.Should().BeTrue();
    }

    [Fact]
    public void Success_ReturnsSuccess() {
        var result = AsSuccess();
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void NotEquals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
        var subject = AsSuccess();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject != AsSuccess();

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, true, false)]
    [InlineData(false, true, true, true)]
    [InlineData(false, false, true, true)]
    [InlineData(false, false, false, false)]
    public void Equals_WithSame_ReturnsAsExpected(bool isNull, bool isSame, bool hasSameError, bool expectedResult) {
        var subject = AsSuccess() + new ValidationError("Some error.", "field");
        var sameValue = AsSuccess() + new ValidationError("Some error.", "field");
        var otherValue = AsSuccess() + new ValidationError("Other error.", "field");

        var result = subject == (isNull ? default : isSame ? subject : hasSameError ? sameValue : otherValue);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        var result = AsSuccess();

        result += new ValidationError("Some error.", "result");

        result.IsSuccess.Should().BeFalse();
        result.HasValidationErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        var result = AsSuccess();

        result += new[] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") };

        result.IsSuccess.Should().BeFalse();
        result.HasValidationErrors.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenSuccess_ReturnsTrue() {
        // Act
        var subject = AsSuccess();

        // Assert
        var result = subject == AsSuccess();

        result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenFailure_ReturnsFalse() {
        // Act
        var subject = AsSuccess() + new ValidationError("Some error.", "field");

        // Assert
        var result = subject == AsSuccess();

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenSuccess_ReturnsFalse() {
        // Act
        var subject = AsSuccess();

        // Assert
        var result = subject != AsSuccess();

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenFailure_ReturnsTrue() {
        // Act
        var subject = AsSuccess() + new ValidationError("Some error.", "field");

        // Assert
        var result = subject != AsSuccess();

        result.Should().BeTrue();
    }
}