using static System.Results.ValidationResult;

namespace System.Results;

public class ValidationResultTests {
    private static readonly ValidationResult _success = Success();
    private static readonly ValidationResult _invalid = Invalid("Some error.", "Source");
    private static readonly ValidationResult _invalidWithSameError = Invalid("Some error.", "Source");
    private static readonly ValidationResult _invalidWithOtherError = Invalid("Other error.", "Source");

    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        // Act
        ValidationResult result = new ValidationError("Some error.", nameof(result));

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        // Act
        ValidationResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        // Act
        ValidationResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    private class TestDataForProperties : TheoryData<ValidationResult, bool, bool> {
        public TestDataForProperties() {
            Add(_invalid, true, false);
            Add(_success, false, true);
        }
    }
    [Theory]
    [ClassData(typeof(TestDataForProperties))]
    public void Properties_ShouldReturnAsExpected(ValidationResult subject, bool isInvalid, bool isSuccess) {
        // Assert
        subject.IsInvalid.Should().Be(isInvalid);
        subject.IsSuccess.Should().Be(isSuccess);
    }

    private class TestDataForEquality : TheoryData<ValidationResult, ValidationResult?, bool> {
        public TestDataForEquality() {
            Add(_success, null, false);
            Add(_success, _success, true);
            Add(_success, _invalid, false);
            Add(_invalid, null, false);
            Add(_invalid, _success, false);
            Add(_invalid, _invalid, true);
            Add(_invalid, _invalidWithSameError, true);
            Add(_invalid, _invalidWithOtherError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForEquality))]
    public void Equals_ReturnsAsExpected(ValidationResult subject, ValidationResult? other, bool expectedResult) {
        // Act
        var result = subject == other;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [ClassData(typeof(TestDataForEquality))]
    public void NotEquals_ReturnsAsExpected(ValidationResult subject, ValidationResult? other, bool expectedResult) {
        // Act
        var result = subject != other;

        // Assert
        result.Should().Be(!expectedResult);
    }

    [Fact]
    public void GetHashCode_DifferentiatesAsExpected() {
        var expectedResult = new HashSet<ValidationResult> {
            _success,
            _invalid,
            _invalidWithOtherError
        };

        // Act
        var result = new HashSet<ValidationResult> {
            Success(),
            Success(),
            _success,
            _success,
            _invalid,
            _invalid,
            _invalidWithSameError,
            _invalidWithOtherError,
        };

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        // Arrange
        var result = Success();

        // Act
        result += new ValidationError("Some error.", "result");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}