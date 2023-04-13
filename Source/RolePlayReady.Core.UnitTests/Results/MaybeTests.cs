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
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsMethodMaybeWithException() {
        Maybe<string> result = default(string);

        result.HasValue.Should().BeFalse();
        result.IsNull.Should().BeTrue();
        result.Default.Should().BeNull();
        result.Invoking(v => v.Value).Should().Throw<InvalidCastException>();
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
}