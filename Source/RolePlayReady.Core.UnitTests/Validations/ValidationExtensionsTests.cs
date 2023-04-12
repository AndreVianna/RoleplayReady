namespace System.Validations;

public class ValidationExtensionsTests {
    [Fact]
    public void Is_ReturnsBuilder() {
        // Arrange
        var subject = "test";

        // Act
        var result = subject.Is();

        // Assert
        result.Should().BeOfType<StringValidator>();
    }

    [Fact]
    public void ListIs_ReturnsBuilder() {
        // Arrange
        var subject = new[] { "test" };

        // Act
        var result = subject.ListIs();

        // Assert
        result.Should().BeOfType<StringsValidator>();
    }

    [Fact]
    public void IteIs_ReturnsBuilder() {
        // Arrange
        var subjects = new[] { "test" };

        // Act
        var result = subjects.ItemIs(v => v.NotNull());

        // Assert
        result.Should().BeOfType<StringsValidator>();
    }
}
