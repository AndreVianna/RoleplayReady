namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        Result<string> result = "testValue";

        result.IsValid.Should().BeTrue();
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
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        Result<string> result = new ValidationError("Some error.", nameof(result));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.Invoking(x => x.Value).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        Result<string> result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        Result<string> result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromFailure_ReturnsFailure() {
        Result<string> result = new Failure(new ValidationError("Some error.", "result"));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidation_ReturnsFailure() {
        Result<string> result = new Validation(new ValidationError("Some error.", "result"));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        Result<string> result = "testValue";

        result += Success.Instance;

        result.IsValid.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        Result<string> result = "testValue";

        result += new ValidationError("Some error.", "result");

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        Result<string> result = "testValue";

        result += new [] { new ValidationError("Some error 1.", "result"), new ValidationError("Some error 2.", "result") } ;

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeTrue();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new Result<string>("testValue");

        string result = input;

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