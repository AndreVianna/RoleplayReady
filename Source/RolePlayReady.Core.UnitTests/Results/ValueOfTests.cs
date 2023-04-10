namespace System.Results;

public class ValueOfTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        Result<string> result = testValue;

        result.HasValue.Should().BeTrue();
        result.Value.Should().Be(testValue);
        result.Invoking(v => v.Exception).Should().Throw<InvalidOperationException>();
        result.Invoking(v => v.Throw()).Should().NotThrow();
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
}