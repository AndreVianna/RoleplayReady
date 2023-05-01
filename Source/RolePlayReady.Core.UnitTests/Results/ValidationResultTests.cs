using static System.Results.ValidationResult;

namespace System.Results;

public class ValidationResultTests {
    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        ValidationResult result = new ValidationError("Some error.", nameof(result));

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        ValidationResult result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        ValidationResult result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
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
        result.HasErrors.Should().BeTrue();
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        var result = AsSuccess();

        result += new[] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
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

//    [Fact]
//    public void ImplicitConversion_FromValue_ReturnsValid() {
//        Result<string> result = "testValue";

//        result.IsSuccess.Should().BeTrue();
//        result.HasErrors.Should().BeFalse();
//        result.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void AddOperator_WithSuccess_ReturnsValid() {
//        Result<string> result = "testValue";

//        result += AsSuccess();

//        result.IsSuccess.Should().BeTrue();
//        result.HasErrors.Should().BeFalse();
//        result.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void Equals_WithSelf_ReturnsTrue() {
//        Result<string> subject = "testValue";

//        // ReSharper disable once EqualExpressionComparison - Want to compare to self.
//#pragma warning disable CS1718, CS0252, CS0253 // Want to compare to self.
//        var result = subject == subject;
//#pragma warning restore CS1718, CS0252, CS0253

//        result.Should().BeTrue();
//    }

//    [Theory]
//    [InlineData(false, true)]
//    [InlineData(true, false)]
//    public void Equals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
//        Result<string> subject = "testValue";
//        if (hasError)
//            subject += new ValidationError("Some error.", "objectResult");

//        var result = subject == AsSuccessFor("testValue");

//        result.Should().Be(expectedResult);
//    }

//    [Theory]
//    [InlineData(false, false)]
//    [InlineData(true, true)]
//    public void OfTValue_NotEquals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
//        Result<string> subject = "testValue";
//        if (hasError)
//            subject += new ValidationError("Some error.", "objectResult");

//        var result = subject != AsSuccessFor("testValue");

//        result.Should().Be(expectedResult);
//    }

//    [Theory]
//    [InlineData(true, true, true, false)]
//    [InlineData(false, true, true, true)]
//    [InlineData(false, false, true, true)]
//    [InlineData(false, false, false, false)]
//    public void OfTValue_Equals_WithSame_ReturnsAsExpected(bool isNull, bool isSame, bool hasSameValue, bool expectedResult) {
//        Result<string> subject = "testValue";
//        Result<string> sameValue = "testValue";
//        Result<string> otherValue = "otherValue";

//        var result = subject == (isNull ? default : isSame ? subject : hasSameValue ? sameValue : otherValue);

//        result.Should().Be(expectedResult);
//    }

//    [Theory]
//    [InlineData(true, true, false)]
//    [InlineData(false, true, false)]
//    [InlineData(false, false, true)]
//    public void NotEquals_WithSame_ReturnsAsExpected(bool isSame, bool hasSameValue, bool expectedResult) {
//        Result<string> subject = "testValue";
//        Result<string> sameValue = "testValue";
//        Result<string> otherValue = "otherValue";

//        var result = subject != (isSame ? subject : hasSameValue ? sameValue : otherValue);

//        result.Should().Be(expectedResult);
//    }

//    [Fact]
//    public void OfTValue_AddOperator_WithError_ReturnsInvalid() {
//        Result<string> result = "testValue";

//        result += new ValidationError("Some error.", "objectResult");

//        result.IsSuccess.Should().BeFalse();
//        result.HasErrors.Should().BeTrue();
//        result.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void AddOperator_FromValidation_ReturnsValid() {
//        var result = AsSuccess();

//        var subject = result.ToResult("testValue");

//        subject.IsSuccess.Should().BeTrue();
//        subject.HasErrors.Should().BeFalse();
//        subject.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void AddOperator_FromValidation_WithErrors_ReturnsInvalid() {
//        var result = AsSuccess() + new ValidationError("Some error.", "someField");

//        var subject = result.ToResult("testValue");

//        subject.IsSuccess.Should().BeFalse();
//        subject.HasErrors.Should().BeTrue();
//        subject.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void AddOperator_FromValidation_WithError_ReturnsInvalid() {
//        Result<string> result = "testValue";
//        var fail = AsSuccess() + new ValidationError("Some error.", "objectResult");

//        var subject = fail + result;

//        subject.IsSuccess.Should().BeFalse();
//        subject.HasErrors.Should().BeTrue();
//        subject.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void OfTValue_AddOperator_WithErrors_ReturnsInvalid() {
//        Result<string> result = "testValue";
//        var fail = AsSuccess() + new[] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") };

//        result += fail;

//        result += new[] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") };

//        result.IsSuccess.Should().BeFalse();
//        result.HasErrors.Should().BeTrue();
//        result.Value.Should().Be("testValue");
//    }

//    [Fact]
//    public void ImplicitConversion_ToValue_BecomesValue() {
//        string result = AsSuccessFor("testValue")!;

//        result.Should().Be("testValue");
//    }

//    [Theory]
//    [InlineData(true, true, false)]
//    [InlineData(false, true, true)]
//    [InlineData(false, false, false)]
//    public void Equality_ShouldReturnAsExpected(bool isNull, bool isSame, bool expectedResult) {
//        var subject = AsSuccessFor("TestValue");
//        var same = AsSuccessFor("TestValue");
//        var other = AsSuccessFor("OtherValue");

//        //Act
//        var result = subject == (isNull ? default : isSame ? same : other);

//        //Assert
//        result.Should().Be(expectedResult);
//    }

//    [Theory]
//    [InlineData(true, true, false)]
//    [InlineData(false, true, true)]
//    [InlineData(false, false, false)]
//    public void Equality_WithError_ShouldReturnAsExpected(bool isNull, bool isSame, bool expectedResult) {
//        var subject = AsInvalidFor("TestValue", "Some error.", "field");
//        var same = AsInvalidFor("TestValue", "Some error.", "field");
//        var other = AsInvalidFor("TestValue", "Other error.", "field");

//        //Act
//        var result = subject == (isNull ? default : isSame ? same : other);

//        result.Should().Be(expectedResult);
//    }

//    [Fact]
//    public void GetHashCode_ShouldCompareCorrectly() {
//        var subject1 = AsInvalidFor("TestValue", "Test error.", "field");
//        var subject2 = AsInvalidFor("TestValue", "Test error.", "field");
//        var subject3 = AsInvalidFor("TestValue", "Other error.", "field");
//        var subject4 = AsInvalidFor("OtherValue", "Test error.", "field");
//        var subject5 = AsInvalidFor("OtherValue", "Other error.", "field");

//        //Act
//        var list = new HashSet<Result<string>> {
//            subject1, 
//            subject1, 
//            subject2, 
//            subject3, 
//            subject4, 
//            subject5,
//        };

//        list.Should().HaveCount(4);
//    }

//    [Fact]
//    public void Result_Map_BecomesNewType() {
//        var input = AsSuccessFor("42");

//        var result = input.Map(int.Parse);

//        result.Value.Should().Be(42);
//    }

//    [Fact]
//    public void Result_Map_WithErrors_BecomesNewType() {
//        var input = AsSuccessFor("42") + new ValidationError("Some error.", "field");

//        var result = input.Map(int.Parse);

//        result.Value.Should().Be(42);
//        result.Errors.Should().HaveCount(1);
//    }

//    [Fact]
//    public void CollectionResult_Map_BecomesNewType() {
//        var input = AsSuccessFor<IEnumerable<string>>(new[] { "42", "7" });

//        var result = input.Map(int.Parse);

//        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
//    }

//    [Fact]
//    public void CollectionResult_Map_WithErrors_BecomesNewType() {
//        var input = AsSuccessFor<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "field");

//        var result = input.Map(int.Parse);

//        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
//        result.Errors.Should().HaveCount(1);
//    }
}