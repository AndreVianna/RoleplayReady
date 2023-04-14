using System.Validators;
using System.Validators.Extensions;

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
        result.Should().BeOfType<CollectionValidator<string>>();
    }
}
