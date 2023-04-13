namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        Result<string> result = testValue;

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(testValue);
    }

    [Fact]
    public void ImplicitConversion_FromNull_ThrowsInvalidCastException() {
        var action = () => {
            Result<string> result = default(string);
        };

        action.Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new Result<string>("testValue");

        string? result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesNull() {
        var action = () => {
            var input = new Result<string>();
            string result = input;
        };

        action.Should().Throw<InvalidCastException>();
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
}