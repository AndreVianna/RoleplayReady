namespace System.Results;

public class ResultTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        Object<string> @object = testValue;

        @object.HasValue.Should().BeTrue();
        @object.Value.Should().Be(testValue);
        @object.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        @object.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromNull_ReturnsMethodResultWithException() {
        Object<string> @object = default(string)!;

        @object.HasValue.Should().BeFalse();
        @object.Exception.Should().NotBeNull();
        @object.Invoking(v => v.Value).Should().Throw<InvalidCastException>();
        @object.Invoking(v => v.Throw()).Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsMethodResultWithException() {
        var testException = new InvalidOperationException("test");
        Object<string> @object = testException;

        @object.HasValue.Should().BeFalse();
        @object.Exception.Should().Be(testException);
        @object.Invoking(v => v.Value).Should().Throw<InvalidOperationException>();
        @object.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ImplicitConversion_ToValue_BecomesValue() {
        var input = new Object<string>("testValue");

        string result = input;

        result.Should().Be("testValue");
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Value_BecomesValue() {
        var input = new Maybe<string>("TestValue");

        Object<string> @object = input;

        @object.HasValue.Should().BeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Null_BecomesException() {
        var input = new Maybe<string>();

        Object<string> @object = input;

        @object.HasValue.Should().BeFalse();
        @object.Invoking(v => v.Throw()).Should().Throw<InvalidCastException>();
    }

    [Fact]
    public void ImplicitConversion_FromMaybe_Exception_BecomesValue() {
        var input = new Maybe<string>(new InvalidOperationException("Some Error"));

        Object<string> @object = input;

        @object.HasValue.Should().BeFalse();
        @object.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Result_Map_BecomesNewType() {
        var input = new Object<string>("42");

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void CollectionResult_Map_BecomesNewType() {
        var input = new Object<IEnumerable<string>>(new[] { "42", "7" });

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(new[] { 42, 7 });
    }

    [Fact]
    public void Result_MapFromException_BecomesException() {
        var input = new Object<string>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CollectionResult_MapFromException_BecomesException() {
        var input = new Object<IEnumerable<string>>(new InvalidOperationException("Some error."));

        var result = input.Map(int.Parse);

        result.HasValue.Should().BeFalse();
        result.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }
}