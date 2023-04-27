using System.Results.Extensions;

using static System.Results.Result;

namespace System.Results;

public class NullableResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        NullableResult<string> result = "testValue";

        result.IsSuccess.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsValid() {
        NullableResult<string> result = default(string);

        result.IsSuccess.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void AddOperator_FromResult_ReturnsInvalid() {
        Result<string> result = "testValue";

        NullableResult<string> subject = result;

        subject.IsSuccess.Should().BeTrue();
        subject.HasErrors.Should().BeFalse();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_ReturnsInvalid() {
        var result = new Result();

        var subject = result.WithNullableValue("testValue");

        subject.IsSuccess.Should().BeTrue();
        subject.HasErrors.Should().BeFalse();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithNull_ReturnsValid() {
        var result = new Result();

        var subject = result.WithNullableValue(default(string));

        subject.IsSuccess.Should().BeTrue();
        subject.HasErrors.Should().BeFalse();
        subject.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromNullableResult_ReturnsAsExpected() {
        NullableResult<object> source = "testValue";
        source += new ValidationError("Some error.", "objectResult");

        NullableResult<string> subject = source;

        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromResult_ReturnsAsExpected() {
        Result<object> source = "testValue";
        source += new ValidationError("Some error.", "objectResult");

        NullableResult<string> subject = source;

        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithErrors_ReturnsInvalid() {
        var result = Success + new ValidationError("Some error.", "someField");

        var subject = result.WithNullableValue("testValue");

        subject.IsSuccess.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithWrongType_ReturnsInvalid() {
        var result = new Result();

        var action = () => {
            NullableResult<string> subject = result.WithNullableValue((object)43);
        };

        action.Should().Throw<InvalidCastException>().WithMessage("Cannot assign 'Result<Integer>' to 'Result<String>'.");
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        NullableResult<string> result = "testValue";

        result += new ValidationError("Some error.", "result");

        result.IsSuccess.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithError_ReturnsInvalid() {
        NullableResult<string> result = "testValue";
        var fail = Success + new ValidationError("Some error.", "objectResult");

        var subject = fail + result;

        subject.IsSuccess.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        string? result = new NullableResult<string>("testValue");

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesNull() {
        string? result = new NullableResult<string>();

        result.Should().BeNull();
    }

    [Fact]
    public void Equals_WithSelf_ReturnsTrue() {
        NullableResult<string> subject = "testValue";

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
        NullableResult<string> subject = "testValue";
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject == Success;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    public void Equals_WithOtherValidation_ReturnsAsExpected(bool hasError, bool expectedResult) {
        NullableResult<string> subject = "testValue";
        var other = new Result();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject == other;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void NotEquals_WithOtherValidation_ReturnsAsExpected(bool hasError, bool expectedResult) {
        NullableResult<string> subject = "testValue";
        var other = new Result();
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject != other;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, true, true, false)]
    [InlineData(false, true, true, true, true)]
    [InlineData(false, false, true, true, true)]
    [InlineData(false, false, false, true, false)]
    public void Equals_WithValue_ReturnsAsExpected(bool isNull, bool isSame, bool hasSameValue, bool otherValueIsNull, bool expectedResult) {
        NullableResult<string> subject = "testValue";
        NullableResult<string> sameValue = "testValue";
        NullableResult<string> otherNull = new();
        NullableResult<string> otherValue = "otherValue";

        var result = subject == (isNull ? default : isSame ? subject : hasSameValue ? sameValue : otherValueIsNull ? otherNull : otherValue);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void Equals_WithNull_ReturnsAsExpected(bool otherValueIsNull, bool expectedResult) {
        NullableResult<string> subject = new();
        NullableResult<string> otherNull = new();
        NullableResult<string> otherValue = "otherValue";

        var result = subject == (otherValueIsNull ? otherNull : otherValue);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(true, true, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public void NotEquals_WithSame_ReturnsAsExpected(bool isSame, bool hasSameValue, bool expectedResult) {
        NullableResult<string> subject = "testValue";
        NullableResult<string> sameValue = "testValue";
        NullableResult<string> otherValue = "otherValue";

        var result = subject != (isSame ? subject : hasSameValue ? sameValue : otherValue);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void NotEquals_WithSameErrors_ReturnsTrue() {
        NullableResult<string> subject = "testValue";
        subject += new ValidationError("Some error.", "objectResult");
        NullableResult<string> other = "testValue";
        other += new ValidationError("Some error.", "objectResult");

        var result = subject == other;

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void NotEquals_WithSuccess_ReturnsAsExpected(bool hasError, bool expectedResult) {
        NullableResult<string> subject = "testValue";
        if (hasError)
            subject += new ValidationError("Some error.", "objectResult");

        var result = subject != Success;

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Equals_WithAnyOtherResult_ReturnsFalse() {
        NullableResult<string> subject = "testValue";
        NullableResult<string> notSelf = "otherValue";

        var result = subject == notSelf;

        result.Should().BeFalse();
    }

    [Fact]
    public void Maybe_Map_BecomesNewType() {
        var input = new NullableResult<string>("42");

        var result = input.Map(int.Parse!);

        result.Value.Should().Be(42);
    }

    [Fact]
    public void Maybe_Map_WithError_BecomesNewType() {
        var input = new NullableResult<string>("42") + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse!);

        result.Value.Should().Be(42);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionMaybe_Map_BecomesNewType() {
        var input = new NullableResult<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void CollectionMaybe_Map_WithError_BecomesNewType() {
        var input = new NullableResult<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse);

        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void GetHashCode_ShouldCompareCorrectly() {
        var subject1 = new NullableResult<string>("TestValue") + new ValidationError("Test error.", "field");
        var subject2 = new NullableResult<string>("TestValue") + new ValidationError("Test error.", "field");
        var subject3 = new NullableResult<string>("TestValue") + new ValidationError("Other error.", "field");
        var subject4 = new NullableResult<string>("OtherValue") + new ValidationError("Test error.", "field");
        var subject5 = new NullableResult<string>("OtherValue") + new ValidationError("Other error.", "field");

        //Act
        var list = new HashSet<NullableResult<string>> { subject1, subject1, subject2, subject3, subject4, subject5, };

        list.Should().HaveCount(4);
    }

    [Fact]
    public void CollectionMaybe_Map_WithNullAndError_BecomesNewType() {
        var input = new NullableResult<IEnumerable<string>>(default) + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse);

        result.Value.Should().BeNull();
        result.Errors.Should().HaveCount(1);
    }
}