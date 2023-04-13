namespace System.Results;

public class VoidResultTests {
    [Fact]
    public void Constructor_CanBeInstantiated() {
        // Act
        VoidResult nothing = Success.Instance;

        // Assert
        nothing.Should().NotBeNull();
        nothing.IsSuccess.Should().BeTrue();
        nothing.Invoking(v => v.Exception).Should().Throw<InvalidCastException>();
        nothing.Invoking(v => v.Throw()).Should().NotThrow();
    }

    [Fact]
    public void ImplicitConversion_FromException_ReturnsNothingWithException() {
        var testException = new InvalidOperationException("test");
        VoidResult nothing = testException;

        nothing.IsSuccess.Should().BeFalse();
        nothing.Exception.Should().Be(testException);
        nothing.Invoking(v => v.Throw()).Should().Throw<InvalidOperationException>();
    }
}