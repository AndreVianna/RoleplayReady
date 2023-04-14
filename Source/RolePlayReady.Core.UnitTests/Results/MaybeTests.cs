namespace System.Results;

public class MaybeTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        Maybe<string> result = "testValue";

        result.IsValid.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsValid() {
        Maybe<string> result = default(string);

        result.IsValid.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        Maybe<string> result = new ValidationError("Some error.", nameof(result));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        Maybe<string> result = new[] { new ValidationError("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        Maybe<string> result = new List<ValidationError> { new("Some error.", nameof(result)) };

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromFailure_ReturnsFailure() {
        Maybe<string> result = new Failure(new ValidationError("Some error.", "result"));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void ImplicitConversion_FromValidation_ReturnsFailure() {
        Maybe<string> result = new Validation(new ValidationError("Some error.", "result"));

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        Maybe<string> result = "testValue";

        result += Success.Instance;

        result.IsValid.Should().BeTrue();
        result.HasErrors.Should().BeFalse();
        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        Maybe<string> result = "testValue";

        result += new ValidationError("Some error.", "result");

        result.IsValid.Should().BeFalse();
        result.HasErrors.Should().BeTrue();
        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new Maybe<string>("testValue");

        string? result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesNull() {
        var input = new Maybe<string>();

        string? result = input;

        result.Should().BeNull();
    }

    [Fact]
    public void Maybe_Map_BecomesNewType() {
        var input = new Maybe<string>("42");

        var result = input.Map(int.Parse!);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void Maybe_Map_WithError_BecomesNewType() {
        var input = new Maybe<string>("42") + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse!);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be(42);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionMaybe_Map_BecomesNewType() {
        var input = new Maybe<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void CollectionMaybe_Map_WithError_BecomesNewType() {
        var input = new Maybe<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionMaybe_Map_WithNullAndError_BecomesNewType() {
        var input = new Maybe<IEnumerable<string>>(default) + new ValidationError("Some error.", "result");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().HaveCount(1);
    }
}