namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        ResultOf<string> resultOf = testValue;

        resultOf.HasValue.Should().BeTrue();
        resultOf.IsNull.Should().BeFalse();
        resultOf.Value.Should().Be(testValue);
        resultOf.Invoking(v => v.Default).Should().Throw<InvalidCastException>();
        resultOf.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        resultOf.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsMethodResultWithException() {
        ResultOf<string> resultOf = default(string);

        resultOf.HasValue.Should().BeFalse();
        resultOf.IsNull.Should().BeTrue();
        resultOf.Default.Should().BeNull();
        resultOf.Invoking(v => v.Value).Should().Throw<InvalidCastException>();
        resultOf.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        resultOf.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsMethodResultWithException() {
        var testException = new InvalidOperationException("test");
        ResultOf<string> resultOf = testException;

        resultOf.HasValue.Should().BeFalse();
        resultOf.IsNull.Should().BeFalse();
        resultOf.Exception.Should().NotBeNull();
        resultOf.Invoking(v => v.Value).Should().Throw<InvalidOperationException>();
        resultOf.Invoking(v => v.Default).Should().Throw<InvalidOperationException>();
        resultOf.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new ResultOf<string>("testValue");

        string? result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesNull() {
        var input = new ResultOf<string>();

        string? result = input;

        result.Should().BeNull();
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new ResultOf<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new ResultOf<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void Result_MapFromException_BecomesException() {
        var input = new ResultOf<string>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CollectionResult_MapFromException_BecomesException() {
        var input = new ResultOf<IEnumerable<string>>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Result_MapFromNull_BecomesException() {
        var input = new ResultOf<string>();

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void CollectionResult_MapFromNull_BecomesException() {
        var input = new ResultOf<IEnumerable<string>>();

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }
}