using System.Results.Extensions;

namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsValid() {
        ObjectResult<string> objectResult = "testValue";

        objectResult.IsSuccessful.Should().BeTrue();
        objectResult.HasErrors.Should().BeFalse();
        objectResult.HasValue.Should().BeTrue();
        objectResult.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsValid() {
        var action = () => {
            ObjectResult<string> objectResult = default(string)!;
        };

        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromValidationError_ReturnsFailure() {
        ObjectResult<string> objectResult = new ValidationError("Some error.", nameof(objectResult));

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeFalse();
        objectResult.Invoking(x => x.Value).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorArray_ReturnsFailure() {
        ObjectResult<string> objectResult = new[] { new ValidationError("Some error.", nameof(objectResult)) };

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidationErrorList_ReturnsFailure() {
        ObjectResult<string> objectResult = new List<ValidationError> { new("Some error.", nameof(objectResult)) };

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromFailure_ReturnsFailure() {
        ObjectResult<string> objectResult = new Failure(new ValidationError("Some error.", "objectResult"));

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromValidation_ReturnsFailure() {
        ObjectResult<string> objectResult = new ValidationResult(new ValidationError("Some error.", "objectResult"));

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeFalse();
    }

    [Fact]
    public void AddOperator_WithSuccess_ReturnsValid() {
        ObjectResult<string> objectResult = "testValue";

        objectResult += Success.Instance;

        objectResult.IsSuccessful.Should().BeTrue();
        objectResult.HasErrors.Should().BeFalse();
        objectResult.HasValue.Should().BeTrue();
        objectResult.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithError_ReturnsInvalid() {
        ObjectResult<string> objectResult = "testValue";

        objectResult += new ValidationError("Some error.", "objectResult");

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeTrue();
        objectResult.Value.Should().Be("testValue");
    }

    [Fact]
    public void AddOperator_WithErrors_ReturnsInvalid() {
        ObjectResult<string> objectResult = "testValue";

        objectResult += new [] { new ValidationError("Some error 1.", "objectResult"), new ValidationError("Some error 2.", "objectResult") } ;

        objectResult.IsSuccessful.Should().BeFalse();
        objectResult.HasErrors.Should().BeTrue();
        objectResult.HasValue.Should().BeTrue();
        objectResult.Value.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new ObjectResult<string>("testValue");

        string result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new ObjectResult<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void Result_Map_WithErrors_BecomesNewType() {
        var input = new ObjectResult<string>("42") + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new ObjectResult<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void CollectionResult_Map_WithErrors_BecomesNewType() {
        var input = new ObjectResult<IEnumerable<string>>(new[] { "42", "7" }) + new ValidationError("Some error.", "field");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
        result.Errors.Should().HaveCount(1);
    }
}