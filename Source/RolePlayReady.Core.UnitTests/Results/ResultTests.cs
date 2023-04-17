using System.Results.Extensions;

using static System.Results.ValidationResult;

namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        Result<string> result = "testValue";

        result.IsSuccessful.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsValid() {
        var action = () => {
            Result<string> result = default(string)!;
        };

        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        Result<string> result = "testValue";

        result += Success;

        result.IsSuccessful.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeTrue();
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
        if (hasError) subject += new ValidationError("Some error.", "objectResult");

        var result = subject == Success;

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    public void Equals_WithOtherValidation_ReturnsAsExpected(bool hasError, bool expectedResult) {
        Result<string> subject = "testValue";
        var other = new ValidationResult();
        if (hasError) subject += new ValidationError("Some error.", "objectResult");

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

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_ReturnsInvalid() {
        var result = new ValidationResult();

        Result<string> subject = result + "testValue";

        subject.IsSuccessful.Should().BeTrue();
        subject.HasErrors.Should().BeFalse();
        subject.HasValue.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithErrors_ReturnsInvalid() {
        var result = Success + new ValidationError("Some error.", "someField");

        Result<string> subject = result + "testValue";

        subject.IsSuccessful.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.HasValue.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_FromValidation_WithWrongType_ReturnsInvalid() {
        var result = new ValidationResult();

        var action = () => {
            Result<string> subject = result + 43;
        };
        

        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void AddOperator_FromValidation_WithError_ReturnsInvalid() {
        Result<string> result = "testValue";
        var fail = Success + new ValidationError("Some error.", "objectResult");

        var subject = fail + result;

        subject.IsSuccessful.Should().BeFalse();
        subject.HasErrors.Should().BeTrue();
        subject.HasValue.Should().BeTrue();
        subject.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        Result<string> result = "testValue";
        var fail = Success + new[] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") };

        result += fail;

        result += new [] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") } ;

        result.IsSuccessful.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        string result = new Result<string>("testValue");

        result.Should().Be("testValue");
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new Result<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void Result_Map_WithErrors_BecomesNewType() {
        var input = new Result<string>("42") + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new Result<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void CollectionResult_Map_WithErrors_BecomesNewType() {
        var input = new Result<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
        result.Errors.Should().HaveCount(1);
    }
}