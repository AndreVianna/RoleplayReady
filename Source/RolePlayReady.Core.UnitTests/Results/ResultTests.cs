namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        Result<string> result = testValue;

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(testValue);
        result.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsMethodResultWithException() {
        Result<string> result = default(string);

        result.HasValue.Should().BeFalse();
        result.Exception.Should().NotBeNull();
        result.Invoking(v => v.Value).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsMethodResultWithException() {
        var testException = new InvalidOperationException("test");
        Result<string> result = testException;

        result.HasValue.Should().BeFalse();
        result.Exception.Should().Be(testException);
        result.Invoking(v => v.Value).Should().Throw<InvalidOperationException>();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new Result<string>("testValue");

        string result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToException_BecomesException() {
        var input = new Result<string>(new InvalidOperationException("Some error."));

        Exception result = input;

        result.Should().BeOfType<InvalidOperationException>();
        result.Message.Should().Be("Some error.");
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Value_BecomesValue() {
        var input = new Maybe<string>("TestValue");

        Result<string> result = input;

        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Null_BecomesException() {
        var input = new Maybe<string>();

        Result<string> result = input;

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Exception_BecomesValue() {
        var input = new Maybe<string>((Exception)new InvalidOperationException("Some Error"));

        Result<string> result = input;

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new Result<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new Result<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }


    [Fact]
    public void Result_MapFromException_BecomesException() {
        var input = new Result<string>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CollectionResult_MapFromException_BecomesException() {
        var input = new Result<IEnumerable<string>>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }
}