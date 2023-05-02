namespace System.Results;

public class CrudResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        // Act
        CrudResult result = new ValidationError("Some error.", nameof(result));

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        // Act
        CrudResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        // Act
        CrudResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Success_ReturnsSuccess() {
        // Act
        var result = CrudResult.Success;

        // Assert
        result.IsSuccess.Should().BeTrue();
    }


    private static readonly CrudResult _success = CrudResult.Success;
    private static readonly CrudResult _notFound = CrudResult.NotFound;
    private static readonly CrudResult _conflict = CrudResult.Conflict;
    private static readonly CrudResult _withError = CrudResult.Invalid("Some error.", "field");
    private static readonly CrudResult _withSameError = new ValidationError("Some error.", "field");
    private static readonly CrudResult _withOtherError = new ValidationError("Other error.", "field");

    private static readonly CrudResult<string> _successForT = CrudResult.SuccessFor("Hello");
    private static readonly CrudResult<string> _notFoundForT = CrudResult.NotFoundFor("Hello");
    private static readonly CrudResult<string> _conflictForT = CrudResult.ConflictFor("Hello");
    private static readonly CrudResult<string> _withErrorForT = CrudResult.InvalidFor("Hello", "Some error.", "field");

    private class TestDataForProperties : TheoryData<CrudResult, bool, bool, bool, bool> {
        public TestDataForProperties() {
            Add(_withError, true, false, false, false);
            Add(_success, false, true, false, false);
            Add(_notFound, false, false, true, false);
            Add(_conflict, false, false, false, true);
            Add(_withErrorForT, true, false, false, false);
            Add(_successForT, false, true, false, false);
            Add(_notFoundForT, false, false, true, false);
            Add(_conflictForT, false, false, false, true);
        }
    }
    [Theory]
    [ClassData(typeof(TestDataForProperties))]
    public void Properties_ShouldReturnAsExpected(CrudResult subject, bool isInvalid, bool isSuccess, bool isNotFound, bool isConflict) {
        // Assert
        subject.IsInvalid.Should().Be(isInvalid);
        subject.IsSuccess.Should().Be(isSuccess);
        if (isInvalid) {
            subject.Invoking(x => x.IsNotFound).Should().Throw<InvalidOperationException>();
            subject.Invoking(x => x.IsConflict).Should().Throw<InvalidOperationException>();
        }
        else {
            subject.IsNotFound.Should().Be(isNotFound);
            subject.IsConflict.Should().Be(isConflict);
        }
    }


    private class TestDataForEquality : TheoryData<CrudResult, CrudResult?, bool> {
        public TestDataForEquality() {
            Add(_success, null, false);
            Add(_success, _success, true);
            Add(_success, _notFound, false);
            Add(_success, _conflict, false);
            Add(_success, _withError, false);
            Add(_notFound, null, false);
            Add(_notFound, _success, false);
            Add(_notFound, _notFound, true);
            Add(_notFound, _conflict, false);
            Add(_notFound, _withError, false);
            Add(_conflict, null, false);
            Add(_conflict, _success, false);
            Add(_conflict, _notFound, false);
            Add(_conflict, _conflict, true);
            Add(_conflict, _withError, false);
            Add(_withError, null, false);
            Add(_withError, _success, false);
            Add(_withError, _notFound, false);
            Add(_withError, _conflict, false);
            Add(_withError, _withError, true);
            Add(_withError, _withSameError, true);
            Add(_withError, _withOtherError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForEquality))]
    public void Equals_ReturnsAsExpected(CrudResult subject, CrudResult? other, bool expectedResult) {
        // Act
        var result = subject == other;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [ClassData(typeof(TestDataForEquality))]
    public void NotEquals_ReturnsAsExpected(CrudResult subject, CrudResult? other, bool expectedResult) {
        // Act
        var result = subject != other;

        // Assert
        result.Should().Be(!expectedResult);
    }

    private class TestDataForBoolImplicitOperator : TheoryData<CrudResult, bool> {
        public TestDataForBoolImplicitOperator() {
            Add(_success, true);
            Add(_withError, false);
        }
    }

    [Theory]
    [ClassData(typeof(TestDataForBoolImplicitOperator))]
    public void ImplicitToBool_ReturnsAsExpected(CrudResult subject, bool expectedResult) {
        // Act
        bool isSuccess = subject;

        // Assert
        isSuccess.Should().Be(expectedResult);
    }

    private class TestDataForValueImplicitOperator : TheoryData<CrudResult<string>, string> {
        public TestDataForValueImplicitOperator() {
            Add(_successForT, "Hello");
            Add(_withErrorForT, "Hello");
        }
    }

    [Fact]
    public void ImplicitFromValue_ReturnsAsExpected() {
        // Act
        var subject = (CrudResult<string>)"Hello";

        // Assert
        subject.Value.Should().Be("Hello");
        subject.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void ImplicitToValue_WithoutError_ReturnsAsExpected() {
        // Arrange
        var subject = CrudResult.SuccessFor("Hello");

        // Act
        var result = (string)subject!;

        // Assert
        result.Should().Be("Hello");
    }

    [Fact]
    public void ImplicitToValue_WithError_ReturnsAsExpected() {
        // Arrange
        var subject = CrudResult.InvalidFor("Hello", "Some error.", "Field");

        // Act
        var action = () => { var _ = (string)subject!; };

        // Assert
        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void GetHashCode_DifferentiatesAsExpected() {
        var expectedResult = new HashSet<CrudResult> {
            _success,
            _withError,
            _withOtherError
        };

        // Act
        var result = new HashSet<CrudResult> {
            CrudResult.Success,
            CrudResult.Success,
            _success,
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
    public void AddOperator_WithoutError_ReturnsInvalid() {
        // Arrange
        var result = CrudResult.Success;

        // Act
        result += ValidationResult.Success;

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        // Arrange
        var result = CrudResult.Success;

        // Act
        result += new ValidationError("Some error.", "result");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithValueAndWithoutError_ReturnsInvalid() {
        // Arrange
        var result = CrudResult.SuccessFor("Hello");

        // Act
        result += ValidationResult.Success;

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsInvalid.Should().BeFalse();
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void AddOperator_WithValueAndWithError_ReturnsInvalid() {
        // Arrange
        var result = CrudResult.SuccessFor("Hello");

        // Act
        result += new ValidationError("Some error.", "result");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsInvalid.Should().BeTrue();
        result.Value.Should().Be("Hello");
    }
}