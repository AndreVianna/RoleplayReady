namespace RolePlayReady.Results;

public class NothingTests {
    [Fact]
    public void ImplicitConversion_FromSuccess_ReturnsNothingWithSuccess() {
        var testSuccess = ResultFactory.Success;
        Nothing nothing = testSuccess;

        nothing.IsSuccessful.Should().BeTrue();
        nothing.Invoking(v => v.Exception).Should().Throw<InvalidOperationException>();
        nothing.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsNothingWithException() {
        var testException = new InvalidOperationException("test");
        Nothing nothing = testException;

        nothing.IsSuccessful.Should().BeFalse();
        nothing.Exception.Should().Be(testException);
        nothing.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }
}