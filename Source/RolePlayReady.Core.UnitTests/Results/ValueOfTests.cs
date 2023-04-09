namespace RolePlayReady.Results;

public class ValueOfTests {
    [Fact]
    public void ImplicitConversion_FromValue_ReturnsMethodResultWithValue() {
        const string testValue = "testValue";
        ValueOf<string> valueOf = testValue;

        valueOf.IsSuccessful.Should().BeTrue();
        valueOf.Value.Should().Be(testValue);
        valueOf.Invoking(v => v.Exception).Should().Throw<InvalidOperationException>();
        valueOf.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsMethodResultWithException() {
        var testException = new InvalidOperationException("test");
        ValueOf<string> valueOf = testException;

        valueOf.IsSuccessful.Should().BeFalse();
        valueOf.Exception.Should().Be(testException);
        valueOf.Invoking(v => v.Value).Should().Throw<InvalidOperationException>();
        valueOf.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }
}