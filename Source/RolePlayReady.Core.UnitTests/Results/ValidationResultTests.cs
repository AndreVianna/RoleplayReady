using static System.Results.ValidationResult;

namespace System.Results;

public class ValidationResultTests {
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

    [Fact]
    public void Success_ReturnsSuccess() {
        // Act
        var result = Success;

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private static readonly ValidationResult _default = Success;
    private static readonly ValidationResult _withoutError = new();
    private static readonly ValidationResult _withError = new ValidationError("Some error.", "field");
    private static readonly ValidationResult _withSameError = new ValidationError("Some error.", "field");
    private static readonly ValidationResult _withOtherError = new ValidationError("Other error.", "field");
    private class TestData : TheoryData<ValidationResult, ValidationResult?, bool> {
        public TestData() {
            Add(_withoutError, null, false);
            Add(_withoutError, _withoutError, true);
            Add(_withoutError, _default, true);
            Add(_withoutError, _withError, false);
            Add(_withError, null, false);
            Add(_withError, _withError, true);
            Add(_withError, _withSameError, true);
            Add(_withError, _default, false);
            Add(_withError, _withoutError, false);
            Add(_withError, _withOtherError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Equals_ReturnsAsExpected(ValidationResult subject, ValidationResult? other, bool expectedResult) {
        // Act
        var result = subject == other;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void NotEquals_ReturnsAsExpected(ValidationResult subject, ValidationResult? other, bool expectedResult) {
        // Act
        var result = subject != other;

        // Assert
        result.Should().Be(!expectedResult);
    }
    private class TestData2 : TheoryData<ValidationResult, bool> {
        public TestData2() {
            Add(Success, true);
            Add(_withoutError, true);
            Add(_withError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestData2))]
    public void ImplicitBool_ReturnsAsExpected(ValidationResult subject, bool expectedResult) {
        // Act
        bool isSuccess = subject;

        // Assert
        isSuccess.Should().Be(expectedResult);
    }

    [Fact]
    public void GetHashCode_DifferentiatesAsExpected() {
        var expectedResult = new HashSet<ValidationResult> {
            _withoutError,
            _withError,
            _withOtherError
        };

        // Act
        var result = new HashSet<ValidationResult> {
            Success,
            Success,
            _withoutError,
            _withoutError,
            _withError,
            _withError,
            _withSameError,
            _withOtherError,
        };

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        // Arrange
        var result = Success;

        // Act
        result += new ValidationError("Some error.", "result");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ToInvalidCrudResult_WithoutError_Throws() {
        // Arrange
        var subject = Success;

        // Assert
        subject.Invoking(x => x.ToInvalidCrudResult(string.Empty)).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ToInvalidCrudResult_WithError_Returns() {
        // Arrange
        var subject = (ValidationResult)new ValidationError("Some error.", "field");

        // Act
        var result = subject.ToInvalidCrudResult(string.Empty);

        // Assert
        result.Should().BeOfType<CrudResult<string>>();
    }

    [Fact]
    public void ToInvalidSignInResult_WithoutError_Throws() {
        // Arrange
        var subject = Success;

        // Assert
        subject.Invoking(x => x.ToInvalidSignInResult()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ToInvalidSignInResult_WithError_Returns() {
        // Arrange
        var subject = (ValidationResult)new ValidationError("Some error.", "field");

        // Act
        var result = subject.ToInvalidSignInResult();

        // Assert
        result.Should().BeOfType<SignInResult>();
    }

    [Fact]
    public void MapTo_WithoutError_ReturnsInvalid() {
        // Arrange
        var subject = CrudResult.SuccessFor("42");

        // Act
        var result = subject.MapTo(int.Parse!);

        // Assert
        result.Should().BeOfType<CrudResult<int>>();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void MapTo_WithError_ReturnsInvalid() {
        // Arrange
        var subject = CrudResult.InvalidFor("42", "Some error.", "Field");

        // Act
        var result = subject.MapTo(int.Parse!);

        // Assert
        result.Should().BeOfType<CrudResult<int>>();
        result.IsSuccess.Should().BeFalse();
    }
}