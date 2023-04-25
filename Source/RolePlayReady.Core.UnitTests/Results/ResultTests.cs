using System.Results.Extensions;

using static System.Results.ValidationResult;

namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        Result<string> result = "testValue";

        result.IsSuccess.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromNull_Throws() {
        var action = () => {
            Result<string> _ = default(string)!;
        };

        action.Should().Throw<InvalidCastException>().WithMessage("Cannot assign null to 'Result<String>'.");
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        Result<string> result = "testValue";

        result += Success;

        result.IsSuccess.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void Equals_WithSelf_ReturnsTrue() {
        Result<string> subject = "testValue";

        // ReSharper disable once EqualExpressionComparison - Want to compare to self.
#pragma warning disable CS1718, CS0252, CS0253 // Want to compare to self.
        var result = subject == subject;
#pragma warning restore CS1718, CS0252, CS0253

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public void Equals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
        Result<string> subject = "testValue";
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject == Success;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    public void Equals_WithOtherValidation_ReturnsAsExpected(bool hasError, bool expectedResult) {
        Result<string> subject = "testValue";
        var other = new ValidationResult();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject == other;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void NotEquals_WithOtherValidation_ReturnsAsExpected(bool hasError, bool expectedResult) {
        Result<string> subject = "testValue";
        var other = new ValidationResult();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject != other;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void NotEquals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
        Result<string> subject = "testValue";
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
    public void Equals_WithSame_ReturnsAsExpected(bool isNull, bool isSame, bool hasSameValue, bool expectedResult) {
        Result<string> subject = "testValue";
        Result<string> sameValue = "testValue";
        Result<string> otherValue = "otherValue";

        var result = subject == (isNull ? default : isSame ? subject : hasSameValue ? sameValue : otherValue);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void NotEquals_WithSame_ReturnsAsExpected(bool isSame, bool hasSameValue, bool expectedResult) {
        Result<string> subject = "testValue";
        Result<string> sameValue = "testValue";
        Result<string> otherValue = "otherValue";

        var result = subject != (isSame ? subject : hasSameValue ? sameValue : otherValue);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        Result<string> result = "testValue";

        result += new ValidationError("Some error.", "objectResult");

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_ReturnsInvalid() {
        var result = new ValidationResult();

        Result<string> subject = result + "testValue";

        subject.IsSuccess.Should().BeTrue();
        subject.HasErrors.Should().BeFalse();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithErrors_ReturnsInvalid() {
        var result = Success + new ValidationError("Some error.", "someField");

        Result<string> subject = result + "testValue";

        subject.IsSuccess.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithWrongType_ReturnsInvalid() {
        var result = new ValidationResult();

        var action = () => {
            Result<string> _ = result + 43;
        };

        action.Should().Throw<InvalidCastException>().WithMessage("Cannot assign 'Result<Integer>' to 'Result<String>'.");
    }

    [Fact]
    public void AddOperator_FromValidation_WithError_ReturnsInvalid() {
        Result<string> result = "testValue";
        var fail = Success + new ValidationError("Some error.", "objectResult");

        var subject = fail + result;

        subject.IsSuccess.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        Result<string> result = "testValue";
        var fail = Success + new[] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") };

        result += fail;

        result += new[] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") };

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        string result = new Result<string>("testValue");

        result.Should().Be("testValue");
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void Equality_ShouldReturnAsExpected(bool isNull, bool isSame, bool expectedResult) {
        var subject = new Result<string>("TestValue");
        var same = new Result<string>("TestValue");
        var other = new Result<string>("OtherValue");

        //Act
        var result = subject == (isNull ? default : isSame ? same : other);

        //Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, true)]
    [InlineData(false, false, false)]
    public void Equality_WithError_ShouldReturnAsExpected(bool isNull, bool isSame, bool expectedResult) {
        var subject = new Result<string>("TestValue") + new ValidationError("Some error.", "field");
        var same = new Result<string>("TestValue") + new ValidationError("Some error.", "field");
        var other = new Result<string>("TestValue") + new ValidationError("Other error.", "field");

        //Act
        var result = subject == (isNull ? default : isSame ? same : other);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void GetHashCode_ShouldCompareCorrectly() {
        var subject1 = new Result<string>("TestValue") + new ValidationError("Test error.", "field");
        var subject2 = new Result<string>("TestValue") + new ValidationError("Test error.", "field");
        var subject3 = new Result<string>("TestValue") + new ValidationError("Other error.", "field");
        var subject4 = new Result<string>("OtherValue") + new ValidationError("Test error.", "field");
        var subject5 = new Result<string>("OtherValue") + new ValidationError("Other error.", "field");

        //Act
        var list = new HashSet<Result<string>> { subject1, subject1, subject2, subject3, subject4, subject5, };

        list.Should().HaveCount(4);
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new Result<string>("42");

        var result = input.Map(int.Parse);

        result.Value.Should().Be(42);
    }

    [Fact]
    public void Result_Map_WithErrors_BecomesNewType() {
        var input = new Result<string>("42") + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.Value.Should().Be(42);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new Result<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void CollectionResult_Map_WithErrors_BecomesNewType() {
        var input = new Result<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
        result.Errors.Should().HaveCount(1);
    }
}