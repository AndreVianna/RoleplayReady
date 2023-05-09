namespace System.Extensions;

public class ValidatableExtensionsTests {
    private class TestValidatable : IValidatable {
        public ValidationResult ValidateSelf(bool negate = false)
            => throw new NotImplementedException();
    }

    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = new TestValidatable();

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<IValidatable?, ValidatableValidators>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        IValidatable? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<IValidatable?, ValidatableValidators>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        IValidatable? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connectors<IValidatable?, ValidatableValidators>>();
    }
}