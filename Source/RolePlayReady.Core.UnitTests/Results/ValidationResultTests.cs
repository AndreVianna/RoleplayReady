using static System.Results.Result;

namespace System.Results;

public class ValidationResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        Result result = new ValidationError("Some error.", nameof(result));

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        Result result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        Result result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void Success_ReturnsSuccess()
        => Success.IsSuccess.Should().BeTrue();

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void NotEquals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
        var subject = new Result();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject != Success;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, true, false)]
    [InlineData(false, true, true, true)]
    [InlineData(false, false, true, true)]
    [InlineData(false, false, false, false)]
    public void Equals_WithSame_ReturnsAsExpected(bool isNull, bool isSame, bool hasSameError, bool expectedResult) {
        var subject = new Result() + new ValidationError("Some error.", "field");
        var sameValue = new Result() + new ValidationError("Some error.", "field");
        var otherValue = new Result() + new ValidationError("Other error.", "field");

        var result = subject == (isNull ? default : isSame ? subject : hasSameError ? sameValue : otherValue);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        var result = new Result();

        result += new ValidationError("Some error.", "result");

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        var result = new Result();

        result += new[] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenSuccess_ReturnsTrue() {
        // Act
        var subject = new Result();

        // Assert
        var result = subject == Success;

        result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WhenFailure_ReturnsFalse() {
        // Act
        var subject = Success + new ValidationError("Some error.", "field");

        // Assert
        var result = subject == Success;

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenSuccess_ReturnsFalse() {
        // Act
        var subject = new Result();

        // Assert
        var result = subject != Success;

        result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WhenFailure_ReturnsTrue() {
        // Act
        var subject = Success + new ValidationError("Some error.", "field");

        // Assert
        var result = subject != Success;

        result.Should().BeTrue();
    }
}