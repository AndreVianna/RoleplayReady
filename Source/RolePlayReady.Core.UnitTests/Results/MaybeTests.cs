namespace System.Results;

public class MaybeTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodMaybeWithValue() {
        const string testValue = "testValue";
        Maybe<string> result = testValue;

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be(testValue);
        result.Invoking(v => v.Default).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsMethodMaybeWithException() {
        Maybe<string> result = default(string);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Default.Should().BeNull();
        result.Invoking(v => v.Value).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsMethodMaybeWithException() {
        var testException = new InvalidOperationException("test");
        Maybe<string> result = testException;

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeFalse();
        result.Exception.Should().NotBeNull();
        result.Invoking(v => v.Value).Should().Throw<InvalidOperationException>();
        result.Invoking(v => v.Default).Should().Throw<InvalidOperationException>();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
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
    public void ImplicitConversion_ToException_BecomesException() {
        var input = new Maybe<string>((Exception)new InvalidOperationException("Some error."));

        Exception result = input;

        result.Should().BeOfType<InvalidOperationException>();
        result.Message.Should().Be("Some error.");
    }

    [Fact]
    public void ImplicitConversion_FromResult_Value_BecomesValue() {
        var input = new Result<string>("TestValue");

        Maybe<string> result = input;

        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromResult_Null_BecomesException() {
        var input = new Result<string>();

        Maybe<string> result = input;

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromResult_Exception_BecomesValue() {
        var input = new Result<string>(new InvalidOperationException("Some Error"));

        Maybe<string> result = input;

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Maybe_Map_BecomesNewType() {
        var input = new Maybe<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.IsNull.Should().BeFalse();
        result.Value.Should().Be(42);
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
    public void Maybe_MapFromException_BecomesException() {
        var input = new Maybe<string>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CollectionMaybe_MapFromException_BecomesException() {
        var input = new Maybe<IEnumerable<string>>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Maybe_MapFromNull_BecomesException() {
        var input = new Maybe<string>();

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void CollectionMaybe_MapFromNull_BecomesException() {
        var input = new Maybe<IEnumerable<string>>();

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Invoking(v => v.Throw()).Should().NotThrow();
    }
}