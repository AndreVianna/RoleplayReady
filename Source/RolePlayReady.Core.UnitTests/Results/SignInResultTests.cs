namespace System.Results;

public class SignInResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        // Act
        SignInResult result = new ValidationError("Some error.", nameof(result));

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        // Act
        SignInResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        // Act
        SignInResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Success_ReturnsSuccess() {
        // Act
        var result = SignInResult.Success("SomeToken");

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private static readonly SignInResult _success = SignInResult.Success("SomeToken");
    private static readonly SignInResult _failure = SignInResult.Failure;
    private static readonly SignInResult _locked = SignInResult.Locked;
    private static readonly SignInResult _blocked = SignInResult.Blocked;
    private static readonly SignInResult _withError = new ValidationError("Some error.", "field");
    private static readonly SignInResult _withSameError = new ValidationError("Some error.", "field");
    private static readonly SignInResult _withOtherError = new ValidationError("Other error.", "field");
    private class TestData : TheoryData<SignInResult, SignInResult?, bool> {
        public TestData() {
            Add(_success, null, false);
            Add(_success, _success, true);
            Add(_success, _failure, false);
            Add(_success, _locked, false);
            Add(_success, _blocked, false);
            Add(_success, _withError, false);
            Add(_failure, null, false);
            Add(_failure, _success, false);
            Add(_failure, _failure, true);
            Add(_failure, _locked, false);
            Add(_failure, _blocked, false);
            Add(_failure, _withError, false);
            Add(_locked, null, false);
            Add(_locked, _success, false);
            Add(_locked, _failure, false);
            Add(_locked, _locked, true);
            Add(_locked, _blocked, false);
            Add(_locked, _withError, false);
            Add(_blocked, null, false);
            Add(_blocked, _success, false);
            Add(_blocked, _failure, false);
            Add(_blocked, _locked, false);
            Add(_blocked, _blocked, true);
            Add(_blocked, _withError, false);
            Add(_withError, null, false);
            Add(_withError, _success, false);
            Add(_withError, _failure, false);
            Add(_withError, _locked, false);
            Add(_withError, _blocked, false);
            Add(_withError, _withError, true);
            Add(_withError, _withSameError, true);
            Add(_withError, _withOtherError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Equals_ReturnsAsExpected(SignInResult subject, SignInResult? other, bool expectedResult) {
        // Act
        var result = subject == other;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void NotEquals_ReturnsAsExpected(SignInResult subject, SignInResult? other, bool expectedResult) {
        // Act
        var result = subject != other;

        // Assert
        result.Should().Be(!expectedResult);
    }

    [Fact]
    public void GetHashCode_DifferentiatesAsExpected() {
        var expectedResult = new HashSet<SignInResult> {
            _success,
            SignInResult.Success("OtherToken"),
            _withError,
            _withOtherError
        };

        // Act
        var result = new HashSet<SignInResult> {
            SignInResult.Success("SomeToken"),
            SignInResult.Success("OtherToken"),
            _success,
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
        var result = SignInResult.Success("SomeToken");

        // Act
        result += new ValidationError("Some error.", "result");

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}