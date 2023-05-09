namespace System.Extensions;

public class IntegerExtensionsTests {
    [Fact]
    public void IsRequired_ReturnsConnector() {
        // Arrange
        var subject = 42;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<int?, IntegerValidators>>();
    }

    [Fact]
    public void IsRequired_ForNullable_ReturnsConnector() {
        // Arrange
        int? subject = default;

        // Act
        var result = subject.IsRequired();

        // Assert
        result.Should().BeOfType<Connectors<int?, IntegerValidators>>();
    }

    [Fact]
    public void IsOptional_ForNullable_ReturnsConnector() {
        // Arrange
        int? subject = default;

        // Act
        var result = subject.IsOptional();

        // Assert
        result.Should().BeOfType<Connectors<int?, IntegerValidators>>();
    }
}