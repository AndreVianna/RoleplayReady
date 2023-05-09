namespace System.Validation.Builder;

public class ValidatorsTests {
    public class TestObject : Validators<long> {
        public TestObject(ValidatorMode mode, long subject, string source, ValidationResult? previousResult = null)
            : base(mode, subject, source, previousResult) {
        }
    }

    [Fact]
    public void Constructor_CreatesBuilder() {
        // Act
        var result = new TestObject(ValidatorMode.Or, 100, "SomeSubject", ValidationResult.Invalid("Some error.", "Source"));

        // Assert
        result.Mode.Should().Be(ValidatorMode.Or);
        result.Result.IsInvalid.Should().BeTrue();
    }

    [Fact]
    public void Constructor_WithoutPreviousError_CreatesBuilder() {
        // Act
        var result = new TestObject(ValidatorMode.Or, 100, "SomeSubject");

        // Assert
        result.Mode.Should().Be(ValidatorMode.Or);
        result.Result.IsSuccess.Should().BeTrue();
    }
}