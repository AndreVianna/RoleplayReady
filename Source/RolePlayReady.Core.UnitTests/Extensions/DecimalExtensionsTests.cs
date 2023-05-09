namespace System.Extensions;

public class DecimalExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = 42.0m;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<decimal?, DecimalValidators>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        decimal? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<decimal?, DecimalValidators>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        decimal? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connectors<decimal?, DecimalValidators>>();
    }
}